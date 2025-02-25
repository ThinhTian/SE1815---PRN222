using System.Diagnostics;
using DemoASPNETCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoASPNETCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeModel message = new HomeModel();
            return View(message);
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
        public IActionResult Create(ProductEditModel model) {
            string msg = "";
            if (ModelState.IsValid)
            {
                msg = "product" + model.Name + " created succesfully!";
            }
            else
            {
                msg = "Failed!";
            }
            return Content(msg);
        }
    }
}
