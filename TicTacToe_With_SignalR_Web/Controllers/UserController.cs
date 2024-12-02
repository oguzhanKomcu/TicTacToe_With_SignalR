using Microsoft.AspNetCore.Mvc;

namespace TicTacToe_With_SignalR_Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
