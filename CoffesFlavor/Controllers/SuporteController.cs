using CoffesFlavor.Services;
using CoffesFlavor.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CoffesFlavor.Controllers
{
    public class SuporteController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;

        public SuporteController(IEmailService emailService, 
            UserManager<IdentityUser> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Index(FormularioHelperViewModel formulario)
        {
            var usuario = await _userManager.FindByNameAsync(User.Identity.Name);
            if (usuario != null)
            {
                var mensagem = new StringBuilder();

                mensagem.Append($"<p>Olá {usuario.UserName}</p>");
                mensagem.Append($"<p>Recebemos sua solicitação de suporte " +
                    $"a respeito do pedido {formulario.NumeroDoPedido}</p>");
                mensagem.Append($"<p>Segue a descrição do chamado: " +
                    $"{formulario.Descricao}</p>");
                mensagem.Append($"<p>A equipe de suporte responsável entrara em contato" +
                    $" pelo devidos meio de comunicação.</p>");
                mensagem.Append($"<p>Atenciosamente, <br />Equipe de Suporte</p>");
                await _emailService.SendEmailAsync(usuario.Email, 
                    $"Solicitação Suporte Pedido N°{formulario.NumeroDoPedido}",
                    "", mensagem.ToString());
                return View(nameof(SolicitacaoSuccessful));
            }
            else
            {
                return View(formulario);
            }
        }

        public IActionResult SolicitacaoSuccessful()
        {
            return View();
        }

    }
}
