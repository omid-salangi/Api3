namespace Application.Interface;

public interface IEmailSender
{
     Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false);
}