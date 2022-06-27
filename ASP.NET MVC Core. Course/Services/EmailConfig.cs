namespace ASP.NET_MVC_Core._Course.Services;

public class EmailConfig
{
    public string FromName { get; set; }
    public string FromAddress { get; set; }
    public string SmtpServer{ get; set; }
    public int Port { get; set; }
    public string User{ get; set; }
    public string Password{ get; set; }
    
}