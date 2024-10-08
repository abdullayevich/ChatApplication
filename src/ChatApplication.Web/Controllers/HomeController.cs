using ChatApplication.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatApplication.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor)
    {
        _contextAccessor = accessor;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.UserName = _contextAccessor.HttpContext?.User.FindFirst("UserName")?.Value;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet("logout")]
    public IActionResult LogOut()
    {
        HttpContext.Response.Cookies.Append("X-Access-Token", "", new CookieOptions()
        {
            Expires = DateTime.Now.AddDays(-1)
        });
        return RedirectToAction("login", "accounts", new { area = "" });
    }
}
