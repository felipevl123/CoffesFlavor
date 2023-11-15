using CoffesFlavor.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CoffesFlavor.Services
{
    public class SendGridService : IEmailService
    {
        private readonly SendGridSettings _emailSettings;

        public SendGridService(IOptions<SendGridSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string emailDestinatario, string assunto, string mensagemTexto, string mensagemHtml)
        {
            var sgc = new SendGridClient(_emailSettings.ApiKey);
            var remetente = new EmailAddress
                (_emailSettings.EmailRemetente, _emailSettings.NomeRemetente);
            var destinatario = new EmailAddress(emailDestinatario);
            var msg = MailHelper
                .CreateSingleEmail(remetente, destinatario, assunto, mensagemTexto, mensagemHtml);
            await sgc.SendEmailAsync(msg);
        }
    }
}
