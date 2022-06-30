using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IEmailService
{
   Task SendAsync(Message message, CancellationToken cancellationToken = default );
}

public class Message
{
    public string Subject { get; set; }
    public string Content { get; set; }
}

