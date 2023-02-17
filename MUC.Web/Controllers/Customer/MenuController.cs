using Microsoft.AspNetCore.Mvc;

namespace MUC.Web.Controllers.Customer
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
