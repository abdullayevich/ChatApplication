using ChatApplication.Service.Dtos.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;

namespace ChatApplication.Web.Views.Accounts.Accounts;

public class AccountsController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public AccountsController(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }
    [HttpGet("register")]
    public ViewResult Register()
    {
        ViewBag.BaseUrl = _configuration["BaseUrl"];
        return View("Register");
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(AccountRegisterDto accountRegisterDto)
    {
        var response = await _httpClient.PostAsJsonAsync<AccountRegisterDto>("https://localhost:7096/api/User/register", accountRegisterDto);
        if (response.IsSuccessStatusCode)
        {
            return View("login");
        }
        return Register();
    }
    [HttpGet("login")]
    public ViewResult Login() => View("Login");

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(AccountLoginDto accountLoginDto)
    {
        var response = await _httpClient.PostAsJsonAsync<AccountLoginDto>("https://localhost:7096/api/User/login", accountLoginDto);
        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(response.);
            HttpContext.Response.Cookies.Append("X-Access-Token", token, new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });
            return View("../Home/Index");
        }
        return View();
    }

}
