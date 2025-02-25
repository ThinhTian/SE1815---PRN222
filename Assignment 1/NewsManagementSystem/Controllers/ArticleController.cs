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
        public ActionResult Create()
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

                var maxNewsArticleId = _context.NewsArticles
                    .OrderByDescending(n => n.NewsArticleId)
                    .FirstOrDefault()?.NewsArticleId;

                string newNewsArticleId = "" + (maxNewsArticleId != null ?
                    (int.Parse(maxNewsArticleId.Substring(2)) + 1).ToString() : "1");

                model.NewsArticleId = newNewsArticleId;

                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;

                try
                {
                    _context.NewsArticles.Add(model);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the article: " + ex.Message);
                    ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(model);
        }

        // GET: ArticleController/Edit/5
        public IActionResult Edit(string id)
        {
            var newsArticle = _context.NewsArticles.Find(id);
            var userId = HttpContext.Request.Cookies["UserId"];
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            if (newsArticle == null)
            {
                return NotFound(); 
            }
            return View(newsArticle);
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NewsArticle newsArticle)
        {

            if (ModelState.IsValid)
            {
                var userIdCookie = HttpContext.Request.Cookies["UserId"];
                if (!string.IsNullOrEmpty(userIdCookie))
                {
                    if (short.TryParse(userIdCookie, out short userId))
                    {
                        var existNews = _context.NewsArticles.FirstOrDefault(c => c.NewsArticleId == newsArticle.NewsArticleId);
                        if (existNews != null)
                        {
                            existNews.UpdatedById = userId;
                            existNews.ModifiedDate = DateTime.Now;
                            existNews.NewsSource = newsArticle.NewsSource;
                            existNews.NewsTitle = newsArticle.NewsTitle;
                            existNews.NewsContent = newsArticle.NewsContent;
                            existNews.NewsStatus = newsArticle.NewsStatus;
                            existNews.CategoryId = newsArticle.CategoryId;
                            existNews.Headline = newsArticle.Headline;

                            _context.Update(existNews);
                            _context.SaveChanges();

                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User ID not found in cookies.");
                }
            }

            return RedirectToAction("Index");

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
