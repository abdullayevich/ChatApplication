using ChatApplication.Service.Hubs;
using ChatApplication.Service.Interfaces;
using ChatApplication.Service.Services;
using ChatApplication.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Add service dependency injection

builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapRazorPages();
//    endpoints.MapControllers();
//});


app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode();
app.MapHub<ChatHub>("/chathub");
app.Run();
