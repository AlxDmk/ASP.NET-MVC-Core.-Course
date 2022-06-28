using System.Net.Mail;
using Domain.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Polly;
using Polly.Registry;
using Polly.Retry;

namespace ASP.NET_MVC_Core._Course.Services;

public class MailKitEmailService:IEmailService
{
    private readonly ILogger<MailKitEmailService> _logger;
    private readonly EmailConfig _settings;
    private readonly ISmtpClient _client;
    private readonly IReadOnlyPolicyRegistry<string> _policyRegistry;

    public MailKitEmailService(
        ILogger<MailKitEmailService> logger,
        IOptions<EmailConfig> settings,
        ISmtpClient client, IReadOnlyPolicyRegistry<string> policyRegistry)
    {
        _logger = logger;
        _client = client;
        _policyRegistry = policyRegistry;
        _settings = settings.Value;
    }
    public void Send(Message message)
    {
        MimeMessage mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
        mimeMessage.To.Add(new MailboxAddress("Администратор","alxdmk@gmail.com"));
        mimeMessage.Subject = message.Subject;
        mimeMessage.Body = new BodyBuilder() {HtmlBody = $"<div>{message.Content}</div>"}.ToMessageBody();

        var res =_policyRegistry.Get<Policy>("StandartPolicy").ExecuteAndCapture(() =>
            {
                _client.Connect(_settings.SmtpServer, _settings.Port, true ); 
                _client.Authenticate(_settings.User, _settings.Password);
                _client.Send(mimeMessage);
                _client.Disconnect(true);
            });

            if (res.Outcome == OutcomeType.Failure)
            {
                _logger.LogError("{F} \n Фатальная ошибка. Сообщение не отправлено! ",res.FinalException.Message);
            }
            else
            {
                _logger.LogWarning("Сообщение отправлено ");
            }

            
    }
}