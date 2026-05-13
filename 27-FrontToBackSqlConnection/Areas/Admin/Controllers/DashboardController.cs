using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _27_FrontToBackSqlConnection.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace _27_FrontToBackSqlConnection.Areas.Controllers
{
    [Area("Admin")]

    public class DashboardController : Controller
    {
        readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

