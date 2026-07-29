public class EnviarEmail
{
    public async Task Email(String EmailDaEmpresa ,String SenhaDoEmail,string EmailCLiente, string assunto, string mensagemTexto)
    {

        try
        {
            using (var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
        {
            client.Credentials = new System.Net.NetworkCredential(EmailDaEmpresa, SenhaDoEmail);
            client.EnableSsl = true;

            var mailMessage = new System.Net.Mail.MailMessage(EmailDaEmpresa, EmailCLiente, assunto, mensagemTexto);
            await client.SendMailAsync(mailMessage);
        }
        }catch (Exception ex)
        {
            Console.WriteLine($"Erro : {ex.Message} ao enviar o email");
        }
    }
}