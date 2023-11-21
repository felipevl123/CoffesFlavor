using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Services;
using CoffesFlavor.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CoffesFlavor.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            AppDbContext context,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Falha ao realizar o login!!");
                return View(loginVM);
            }
            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            

            if (user != null)
            {
                if (!_userManager.IsEmailConfirmedAsync(user).Result)
                {
                    ModelState.AddModelError("", "Este usuário ainda não confirmou sua conta. " +
                        "Confirme e tente novamente.");
                    return View(loginVM);
                }

                var result = await _signInManager
                    .PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Falha ao realizar o login!!");
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.Users
                    .Any(u => u.NormalizedEmail == registroVM.Email.ToUpper().Trim()))
                {
                    ModelState.AddModelError("",
                                "Já existe um usuário cadastrado com este e-mail.");
                    return View(registroVM);
                }

                var user = new IdentityUser
                {
                    UserName = registroVM.UserName,
                    Email = registroVM.Email,
                    PhoneNumber = registroVM.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                    _context.ContaDetalhes.Add
                        (new ContaDetalhe
                        {
                            AspNetUsersId = user.Id,
                            DataDeNascimento = registroVM.DataDeNascimento,
                            UserName = user.UserName,
                            Email = user.Email
                        });
                    _context.SaveChanges();
                    await EnviarLinkConfirmacaoEmailAsync(user);
                    return RedirectToAction("RegisterSuccessful", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("", "Falha ao registrar o usuário");
                }
            }
            return View(registroVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EsqueciSenha([FromForm] EsqueciSenhaViewModel dados)
        {
            if (ModelState.IsValid)
            {
                if (!_userManager.Users.AsNoTracking().Any(u => u.NormalizedEmail == dados.Email
                .ToUpper().Trim()))
                {
                    ModelState.AddModelError("Email", "Não existe usuário com o email informado.");
                    return View(dados);   
                }
                else
                {
                    var usuario = await _userManager.FindByEmailAsync(dados.Email);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    var urlConfirmacao = Url.Action(nameof(RedefinirSenha), "Account",
                        new { token }, Request.Scheme);
                    var mensagem = new StringBuilder();
                    mensagem.Append($"<p>Olá {usuario.UserName}</p>");
                    mensagem.Append($"<p>Houve uma solicitação de redefinição de senha para o seu " +
                        $"usuário em nosso site.Se não foi voce que fez a solicitação, ignore " +
                        $"essa mensagem. Caso tenha sido você, clique no link abaixo para" +
                        $" criar sua nova senha: </p>");
                    mensagem.Append($"<p><a href='{urlConfirmacao}'>Redefinir Senha</a></p>");
                    mensagem.Append("<p>Atenciosamente, <br/> Equipe de Suporte</p>");
                    await _emailService.SendEmailAsync(usuario.Email, "Redefinição de Senha",
                        "", mensagem.ToString());
                    return View(nameof(EmailRedefinicaoEnviado));
                }
            }
            else
            {
                return View(dados);
            }
        }

        public IActionResult EmailRedefinicaoEnviado()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RedefinirSenha(string token)
        {
            var modelo = new RedefinirSenhaViewModel();
            modelo.Token = token;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult>
            RedefinirSenha([FromForm] RedefinirSenhaViewModel dados)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(dados.Email);
                if(usuario == null)
                {
                    ModelState
                        .AddModelError("Email", "Não foi possivel redefinir senha, verifique o email informado");
                    return View(dados);
                }
                var resultado = await _userManager
                    .ResetPasswordAsync(usuario, dados.Token, dados.NovaSenha);
                if (resultado.Succeeded)
                {
                    return View(nameof(Login));
                }
                else
                {
                    return View(dados);
                }
            }
            else
            {
                return View(dados);
            }
        }

        private async Task EnviarLinkConfirmacaoEmailAsync(IdentityUser usuario)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            var urlConfirmacao = Url.Action("ConfirmarEmail", "Account", 
                new { email = usuario.Email, token}, Request.Scheme);
            var mensagem = new StringBuilder();

            mensagem.Append($"<p>Olá {usuario.UserName}</p>");
            mensagem.Append($"<p>Recebemos seu cadastro em nosso sistema. Para concluir o" +
                $" processo de cadastro, clique no link a seguir:</p>");
            mensagem.Append($"<p><a href='{urlConfirmacao}'>Confirmar Cadastro</a></p>");
            mensagem.Append($"<p>Atenciosamente, <br />Equipe de Suporte</p>");
            await _emailService.SendEmailAsync(usuario.Email, "Confirmação de cadastro",
                "", mensagem.ToString());
        }

        public IActionResult RegisterSuccessful()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarEmail(string email, string token)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if(usuario == null)
            {
                ModelState
                        .AddModelError("", "Não foi possivel confirmar o email informado, usuário não encontrado.");
                return RedirectToAction("Login", "Account");
            }

            var resultado = await _userManager.ConfirmEmailAsync(usuario, token);
            if (!resultado.Succeeded)
            {
                ModelState
                        .AddModelError("", "Não foi possivel validar seu e-mail. Tente novamente em alguns minutos.");
                return RedirectToAction("Login", "Account");
            }

            TempData["Sucesso"] = "Seu email foi confirmado com sucesso";
            return View(nameof(Login));
        }
    }
}