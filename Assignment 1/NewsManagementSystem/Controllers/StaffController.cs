using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsManagementSystem.Models;

namespace NewsManagementSystem.Controllers
{
    public class StaffController : Controller
    {
        private readonly FunewsManagementContext _context;

        public StaffController(FunewsManagementContext context)
        {
            _context = context;
        }

        // GET: StaffController
        public IActionResult StaffDashboard()
        {
            var userRole = HttpContext.Request.Cookies["UserRole"];
            var userId = HttpContext.Request.Cookies["UserId"];

            if (userRole != null)
            {
                if (userRole == "Staff")
                {
                    return View();
                }
            }

            return RedirectToAction("Login", "Account");
        }


        // GET: StaffController/Details/5
        public ActionResult ViewHistory()
        {
            var userIdCookie = HttpContext.Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie))
            {
                if (short.TryParse(userIdCookie, out short userId))
                {
                    var userCreatedArticles = _context.NewsArticles
                                                      .Where(n => n.CreatedById == userId)
                                                      .Include(n => n.Category)
                                                      .ToList();

                    return View(userCreatedArticles);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User ID format.");
                    return View(new List<NewsArticle>());
                }
            }
            else
            {
                ModelState.AddModelError("", "User ID not found in cookies.");
                return View(new List<NewsArticle>());
            }
        }


    }
}
