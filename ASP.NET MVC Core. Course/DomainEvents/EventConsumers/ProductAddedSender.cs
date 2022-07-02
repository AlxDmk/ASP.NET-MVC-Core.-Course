using Domain.Interfaces;

namespace ASP.NET_MVC_Core._Course.DomainEvents.EventConsumers;

public  class ProductAddedHandler : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private CancellationToken _stoppingToken;

    public  ProductAddedHandler(ILogger<ProductAddedHandler> logger, IServiceScopeFactory serviceScopeFactory)
    {
       _serviceScopeFactory = serviceScopeFactory;
       DomainEvents.Register<ProductAdded>(ev => _ = SendEmailProductAddedNotification(ev));
    }
    private async Task SendEmailProductAddedNotification(ProductAdded ev)
    {
        CancellationToken cancellationToken = default;
        
        await using var scoped = _serviceScopeFactory.CreateAsyncScope();
        var emailSender = scoped.ServiceProvider.GetService<IEmailService>();
        await emailSender.SendAsync(new Message() {
                Subject = "Добавление товара в католог",
                Content = $"Товар {ev.Product.Id} добавлен в каталог!"
            }, cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _stoppingToken = stoppingToken;
        return Task.CompletedTask;
    }
}