using ASP.NET_MVC_Core._Course.Controllers;
using ASP.NET_MVC_Core._Course.Repository;
using ASP.NET_MVC_Core._Course.Services;
using ASP.NET_MVC_Core._Course.ViewModels.Mapping;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IRepositories;
using MailKit.Net.Smtp;
using Polly;
using Polly.Registry;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Log.Information("Сервис запущен!");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
    var mapper = mapperConfiguration.CreateMapper();

    var eConfig = builder.Services
        .Configure<EmailConfig>(builder.Configuration.GetSection("Email"))
        .Configure<EmailConfig>(x =>
        {
            x.User = builder.Configuration["Email:User"];
            x.Password = builder.Configuration["Email:Password"];
        });
    builder.Services.AddSingleton(eConfig);

    builder.Services.AddSingleton<IRepository<Category>, CategoryRepositoryList>();
    builder.Services.AddSingleton<IRepository<Product>, ProductRepositoryList>();

    builder.Services.AddScoped<IEmailService, MailKitEmailService>();
    builder.Services.AddTransient<ISmtpClient, SmtpClient>();

    builder.Host.UseSerilog((context, conf) =>
    {
        conf.ReadFrom.Configuration(context.Configuration);
    });

    PolicyRegistry policyRegistry = new PolicyRegistry();
    Policies policies = new Policies();
    policyRegistry.Add("StandartPolicy", policies.standartPolicy);
    
    builder.Services.AddSingleton<IReadOnlyPolicyRegistry<string>>(policyRegistry);
    
    builder.Services.AddSingleton(mapper);

    builder.Services.AddControllersWithViews();

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSerilogRequestLogging();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch(Exception ex)
{
    Log.Fatal(ex,"Сервис завалился!");
}
finally
{
    Log.CloseAndFlush();
    
}
    

