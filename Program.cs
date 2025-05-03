using System;
using BookApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//var builder = WebApplication.CreateBuilder(args);

//// Local host URL
//builder.WebHost.UseUrls("http://localhost:5000");

//// Singleton JSON repository
//builder.Services.AddSingleton<IBookRepository, JsonBookRepository>();

//// HTTP client + Google Books service
//builder.Services.AddHttpClient<IGoogleBooksService, GoogleBooksService>(client => {
//    var cfg = builder.Configuration.GetSection("GoogleBooks");
//    client.BaseAddress = new Uri(cfg["ApiBaseUrl"]!);
//});

////mvc
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Ensure data file
//JsonBookRepository.InitializeDataFile(app.Environment.ContentRootPath);

//if (!app.Environment.IsDevelopment())
//    app.UseExceptionHandler("/Home/Error");

//app.UseStaticFiles();
//app.UseRouting();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Books}/{action=Index}/{id?}");

//app.Run();

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");

builder.Services.AddSingleton<IBookRepository, JsonBookRepository>();
builder.Services.AddHttpClient<IGoogleBooksService, GoogleBooksService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["GoogleBooks:ApiBaseUrl"]!);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

JsonBookRepository.InitializeDataFile(app.Environment.ContentRootPath);

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();