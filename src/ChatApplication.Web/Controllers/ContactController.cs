using ChatApplication.Domain.Entities;
using ChatApplication.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _httpClient;

        public ContactController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ViewResult> Index()
        {
            var users = await _httpClient.GetFromJsonAsync<IList<AllUserViewModel>>("https://localhost:7096/api/User/get_all");
            return View("Index", users);
        }
        [HttpGet]
        public async Task<IActionResult> DirectChat(string username)
        {
            ViewBag.UserName = username;
            return View("DirectChat");
        }
    }
}
