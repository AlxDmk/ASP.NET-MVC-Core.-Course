namespace Domain.Interfaces;

public interface IEmailService
{
    void Send(Message message);
}

public class Message
{
    public string Subject { get; set; }
    public string Content { get; set; }
}

