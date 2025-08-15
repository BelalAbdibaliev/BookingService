using System.Net;
using System.Net.Mail;
using BS.Application.Interfaces;

namespace BS.Infrastructure.Services;

public class SmtpEmailSender: IEmailSender
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _fromEmail;
    private readonly string _password;

    public SmtpEmailSender(string smtpServer, int smtpPort, string fromEmail, string password)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _fromEmail = fromEmail;
        _password = password;
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            using var client = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_fromEmail, _password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage(_fromEmail, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mailMessage);
            return true;
        }
        catch
        {
            return false;
        }
    }

}