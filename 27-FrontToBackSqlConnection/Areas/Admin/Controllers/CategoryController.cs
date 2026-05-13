using Microsoft.AspNetCore.Mvc;

namespace _27_FrontToBackSqlConnection.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
