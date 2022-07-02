using Domain.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Polly;
using Polly.Registry;

namespace ASP.NET_MVC_Core._Course.Services;

public class MailKitEmailService:IEmailService, IDisposable, IAsyncDisposable
{
    private readonly ILogger<MailKitEmailService> _logger;
    private readonly EmailConfig _settings;
    private readonly ISmtpClient _client;
    private readonly IReadOnlyPolicyRegistry<string> _policyRegistry;
    

    public MailKitEmailService(
        ILogger<MailKitEmailService> logger,
        IOptions<EmailConfig> settings,
        ISmtpClient client, 
        IReadOnlyPolicyRegistry<string> policyRegistry)
    
    {
        _logger = logger;
        _client = client;
        _policyRegistry = policyRegistry;
        _settings = settings.Value;
    }
    public async  Task SendAsync(Message message, CancellationToken cancellationToken = default)
    {
        EnsureConnectedAndAuthenticated(cancellationToken);
        
        MimeMessage mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
        mimeMessage.To.Add(new MailboxAddress("Администратор","alxdmk@gmail.com"));
        mimeMessage.Subject = message.Subject;
        mimeMessage.Body = new BodyBuilder() {HtmlBody = $"<div>{message.Content}</div>"}.ToMessageBody();

        var res = await _policyRegistry.Get<AsyncPolicy>("StandartPolicyAsync")
            .ExecuteAndCaptureAsync(async () =>
                await _client.SendAsync(mimeMessage, cancellationToken));

        if (res.Outcome == OutcomeType.Failure)
        {
            _logger.LogError("{F} \n Фатальная ошибка. Сообщение не отправлено! ",res.FinalException.Message);
        }
        else
        {
            _logger.LogWarning("Сообщение отправлено ");
        }
    }

    private void EnsureConnectedAndAuthenticated(CancellationToken cancellationToken)
    {
          
           EnsureConnected(cancellationToken);
           EnsureAuthenticated(cancellationToken);
        
    }

    private void EnsureConnected(CancellationToken cancellationToken)
    {
        if (!_client.IsConnected)
        {
              _client.Connect(_settings.SmtpServer, _settings.Port, cancellationToken: cancellationToken );
              _logger.LogWarning("Connection OK");
        }
    }

    private void  EnsureAuthenticated(CancellationToken cancellationToken)
    {
        if (!_client.IsAuthenticated)
        {
             _client.Authenticate(_settings.User, _settings.Password, cancellationToken: cancellationToken);
            _logger.LogWarning("Authenticate OK");
            
        }
    }

    public  void Dispose()
    {
        if (_client.IsConnected  )
        {
            _client.DisconnectAsync(true);
            _logger.LogWarning("Disposed");
        }
        _client.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_client.IsConnected  )
        {
            await  _client.DisconnectAsync(true);
            _logger.LogWarning("Disposed");
        }
        _client.Dispose();
    }
}