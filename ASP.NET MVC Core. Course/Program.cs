using ASP.NET_MVC_Core._Course.Services;
using ASP.NET_MVC_Core._Course.ViewModels.Mapping;
using AutoMapper;
using Data.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IRepositories;
using MailKit.Net.Smtp;


var builder = WebApplication.CreateBuilder(args);

var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
var mapper = mapperConfiguration.CreateMapper();

var emailConfig = builder.Configuration
    .GetSection("Email")
    .Get<EmailConfig>();
builder.Services.AddSingleton(emailConfig);


builder.Services.AddSingleton<IRepository<Category>, CategoryRepositoryList>();
builder.Services.AddSingleton<IRepository<Product>, ProductRepositoryList>();
builder.Services.AddSingleton<IEmailService, MailKitEmailService>();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();