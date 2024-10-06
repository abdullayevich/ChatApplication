using ChatApplication.Service.Configuration;
using ChatApplication.Service.Hubs;
using ChatApplication.Service.Interfaces;
using ChatApplication.Service.Middlewares;
using ChatApplication.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddWeb(builder.Configuration);
// Add service dependency injection
builder.Services.AddService();



// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddControllers();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors("MyAllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<ChatHub>("/chatHub");
//});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TokenRedirectMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
