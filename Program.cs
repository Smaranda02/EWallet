using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using EWallet.BusinessLogic.Implementation.Users;
using EWallet.DataAccess.EntityFramework;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using EWallet.BusinessLogic;
using EWallet.DataAccess;
using EWallet.WebApp.Code;
using EWallet.WebApp.Code.ExtensionMethods;
using EWallet.Entities.Entities;
using System.Web.WebPages;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
});




builder.Services.AddAutoMapper(options =>
{
    options.AddMaps(typeof(Program), typeof(BaseService));
});

builder.Services.AddDbContext<EWalletContext>();

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddEWalletCurrentUserViewModel();

builder.Services.AddPresentation();

builder.Services.AddEWalletBusinessLogic();

builder.Services.AddAuthentication("EWalletCookies")
       .AddCookie("EWalletCookies", options =>
       {
           options.AccessDeniedPath = new PathString("/User/Login");
           options.LoginPath = new PathString("/User/Login");
       });
        


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



//app.Use(async (context, next) =>
//{
//    await next();
//    if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
//    {
//        context.Request.Path = "/Error/NotFound";
//        await next();
//    }
//    else if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
//    {
//        context.Request.Path = "/Error/Unauthorized";
//        await next();
//    }
//    else if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
//    {
//        context.Request.Path = "/Error/InternalServerError";
//        await next();
//    }
//    else if (context.Response.StatusCode.ToString().StartsWith("4") || context.Response.StatusCode.ToString().StartsWith("5"))
//    {
//        context.Request.Path = "/Error";
//        await next();
//    }
//});

//app.UseCors("HomeFactoryPolicy");


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");


app.Run();


