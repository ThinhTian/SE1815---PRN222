using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsManagementSystem.Models;

namespace NewsManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        private readonly FunewsManagementContext _context;
        public AdminController(FunewsManagementContext context)
        {
            _context = context;
        }
        public IActionResult AdminManagement()
        {
            var userInformation = _context.SystemAccounts.ToList();
            return View(userInformation);
        }
        public IActionResult GenerateReport()
        {
            return View();
        }
        public IActionResult ReportStatistics(DateTime startDate, DateTime endDate)
        {
            var reportData = _context.NewsArticles
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();

            return View(reportData);
        }

        // GET: AdminController/Details/5
        public IActionResult Details(string id)
        {
            var newsArticle = _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .FirstOrDefault(n => n.NewsArticleId == id);

            return View(newsArticle);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AdminController/Edit/5
        [HttpGet]
        public IActionResult Edit(int AccountId)
        {
            var account = _context.SystemAccounts.FirstOrDefault(a => a.AccountId == AccountId);
            if (account == null) {
                return NotFound();
            }
            return View("Edit", account);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SystemAccount model)
        {
            var accountName = _context.SystemAccounts.FirstOrDefault(a => a.AccountName == model.AccountName);
            if (accountName != null)
            {
                accountName.AccountRole = model.AccountRole;
                accountName.Status = model.Status;
                _context.SaveChanges();
            }
            return RedirectToAction("AdminManagement");
        }
    }
}
