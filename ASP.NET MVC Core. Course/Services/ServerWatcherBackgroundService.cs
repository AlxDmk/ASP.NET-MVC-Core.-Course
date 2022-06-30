using Domain.Interfaces;

namespace ASP.NET_MVC_Core._Course.Services;

public class ServerWatcherBackgroundService: BackgroundService
{
   
    private readonly ILogger<ServerWatcherBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;
    

    public ServerWatcherBackgroundService(ILogger<ServerWatcherBackgroundService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromHours(1));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            stoppingToken.ThrowIfCancellationRequested();
            await using var scope = _serviceProvider.CreateAsyncScope();
            var emailSender = scope.ServiceProvider.GetRequiredService<IEmailService>();
            await emailSender.SendAsync(new Message()
            {
                Subject = "Сообщение от фонового сервиса",
                Content = "Сервис работает в фоновом режиме"
                    
            }, stoppingToken);
            _logger.LogWarning("Фоновый сервис отправил сообщение");
        }

    }
}