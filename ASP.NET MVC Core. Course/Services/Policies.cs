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
      public AsyncRetryPolicy standartPolicyAsync = Policy
        .Handle<Exception>()
        .RetryAsync(2, (ex, retryAttempt) => 
            Log.Warning("{A}  Попытка {B}", ex.Message, retryAttempt)
            );
      public AsyncRetryPolicy exponantionalPolicyAsync = Policy
          .Handle<Exception>()
          .WaitAndRetryAsync(2,
              retryAttempt => TimeSpan.FromSeconds(Math.Pow(retryAttempt, 2)),
              (ex, retryAttempt) => 
              Log.Warning("{A}  Попытка {B}", ex.Message, retryAttempt)
          ); 
}