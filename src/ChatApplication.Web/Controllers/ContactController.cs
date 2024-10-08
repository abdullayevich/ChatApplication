using ChatApplication.Domain.Entities;
using ChatApplication.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Web.Controllers;
[Authorize]
public class ContactController : Controller
{
    private readonly IHttpContextAccessor _accessor;
    private readonly HttpClient _httpClient;


    public ContactController(HttpClient httpClient, IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        _httpClient = httpClient;
    }
    public async Task<ViewResult> Index()
    {
		ViewBag.UserName = _accessor.HttpContext?.User.FindFirst("UserName")?.Value;
		var userId = int.Parse(_accessor.HttpContext!.User.FindFirst("Id").Value);
        var users = await _httpClient.GetFromJsonAsync<IList<AllUserViewModel>>("https://localhost:7096/api/User/get_all");
        var result = users.Where(x => x.Id != userId);

        return View("Index", result);
    }
    [HttpGet]
    public async Task<IActionResult> DirectChat(string username)
    {
		ViewBag.UserName1 = username;
		ViewBag.UserName = _accessor.HttpContext?.User.FindFirst("UserName")?.Value;
        return View("DirectChat");
    }
}
