using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsManagementSystem.Models;

namespace NewsManagementSystem.Controllers
{
    public class ArticleController : Controller
    {
        private readonly FunewsManagementContext _context;

        public ArticleController(FunewsManagementContext context)
        {
            _context = context;
        }

        // GET: ArticleController
        public IActionResult Index()
        {
            var userRole = HttpContext.Request.Cookies["UserRole"];
            var userId = HttpContext.Request.Cookies["UserId"];
            var news = _context.NewsArticles.ToList();
            return View(news);
        }

        // GET: ArticleController/Details/5
        public IActionResult Details(string id)
        {
            var newsArticle = _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .FirstOrDefault(n => n.NewsArticleId == id);

            return View(newsArticle);
        }

        // GET: ArticleController/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            var userId = HttpContext.Request.Cookies["UserId"];

            var model = new NewsArticle
            {
                CreatedDate = DateTime.Now
            };

            return View(model);
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsArticle model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Request.Cookies["UserId"];

                if (!string.IsNullOrEmpty(userId))
                {
                    if (short.TryParse(userId, out short parsedUserId))
                    {
                        model.CreatedById = parsedUserId;
                        model.UpdatedById = parsedUserId;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid user ID");
                        ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                        return View(model);
                    }
                }
                _context.NewsArticles.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(model);
        }


        // GET: ArticleController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArticleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
