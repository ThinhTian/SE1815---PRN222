using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsManagementSystem.Models;

namespace NewsManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly FunewsManagementContext _context;

        public CategoryController(FunewsManagementContext context){
            _context = context;
        }

        // GET: CategoryController
        public IActionResult Category()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }


        // GET: CategoryController/Create
        public ActionResult Create()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View();
        }


        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Category");
            }

            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View(category);
        }


        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null) {
                NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                if (existingCategory != null)
                {
                    existingCategory.CategoryName = category.CategoryName;
                    existingCategory.CategoryDesciption = category.CategoryDesciption;
                    existingCategory.IsActive = category.IsActive;

                    _context.Update(existingCategory);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Category");
        }


        // POST: Category/Delete/5
        [HttpPost]
        public IActionResult Delete(short id)
        {
            
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Category");
        }

    }
}
