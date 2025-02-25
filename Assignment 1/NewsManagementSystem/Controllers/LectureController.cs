using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsManagementSystem.Models;

namespace NewsManagementSystem.Controllers
{
    public class LectureController : Controller
    {

        private readonly FunewsManagementContext _context;

        public LectureController(FunewsManagementContext context)
        {
            _context = context;
        }

        // GET: LectureController
        public IActionResult Lecture()
        {
            var lectures = _context.NewsArticles.ToList();
            return View(lectures);
        }

        // GET: LectureController/Details/5
        public IActionResult Details(string NewArticleId)
        {
            var newsArticle = _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .FirstOrDefault(n => n.NewsArticleId == NewArticleId);

            return View(newsArticle);
        }

    }
}
