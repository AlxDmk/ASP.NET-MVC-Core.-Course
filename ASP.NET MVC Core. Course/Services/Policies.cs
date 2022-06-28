using System.Net.Mail;
using ASP.NET_MVC_Core._Course.Controllers;
using Polly;
using Polly.Retry;
using Polly.Wrap;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ASP.NET_MVC_Core._Course.Services;

public  class Policies
{
    public  Policy standartPolicy = Policy
        .Handle<Exception>()
        .Retry(2, (ex, retryAttempt) => 
            Log.Warning("{A}  Попытка {B}", ex.Message, retryAttempt)
            );
}