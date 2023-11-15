namespace CoffesFlavor.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync
            (string emailDestinatario, string assunto, string mensagemTexto, string mensagemHtml);
    }
}


//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