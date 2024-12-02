using Microsoft.AspNetCore.Mvc;

namespace TicTacToe_With_SignalR_Web.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
