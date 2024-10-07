using ChatApplication.Service.Configuration;
using ChatApplication.Service.Interfaces;
using ChatApplication.Service.Middlewares;
using ChatApplication.Service.Services;
using ChatApplication.Web.Configuration;
using ChatApplication.Web.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddHttpClient("ChatAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7096"); // API xizmati manzili
});
builder.Services.AddSignalR();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddWeb(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<TokenRedirectMiddleware>();

app.UseStatusCodePages(async context=>
{
    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        context.HttpContext.Response.Redirect("/login");
    }
});



app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapHub<ChatHub>("/ChatHub");

app.Run();
