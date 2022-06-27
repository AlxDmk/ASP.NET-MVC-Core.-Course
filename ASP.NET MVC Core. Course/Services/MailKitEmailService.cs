using Domain.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ASP.NET_MVC_Core._Course.Services;

public class MailKitEmailService:IEmailService
{
    private readonly ILogger<MailKitEmailService> _logger;
    private readonly EmailConfig _settings;

    public MailKitEmailService(ILogger<MailKitEmailService> logger, EmailConfig settings)
    {
        _logger = logger;
        _settings = settings;
    }
    public void Send(Message message)
    {
        MimeMessage mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
        mimeMessage.To.Add(new MailboxAddress("Администратор","alxdmk@gmail.com"));
        mimeMessage.Subject = message.Subject;
        mimeMessage.Body = new BodyBuilder() {HtmlBody = $"<div>{message.Content}</div>"}.ToMessageBody();
        try
        {
            using var client = new SmtpClient();
            
            client.Connect(_settings.SmtpServer, _settings.Port, true );
            client.Authenticate(_settings.User, _settings.Password);
            client.Send(mimeMessage);
            client.Disconnect(true);
            _logger.LogInformation("Сообщение отправлено");
        }
        catch (Exception e)
        {
            
            _logger.LogError(e.GetBaseException().Message);
        }
    }

    
}