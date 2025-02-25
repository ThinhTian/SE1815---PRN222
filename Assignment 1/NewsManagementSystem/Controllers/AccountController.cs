using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsManagementSystem.Models;
using System;

namespace NewsManagementSystem.Controllers
{

    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private FunewsManagementContext context = new FunewsManagementContext();
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: AccountController
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string username = model.Username;
                string password = model.Password;

                if (username == _configuration["DefaultAccount:email"] && password == _configuration["DefaultAccount:password"])
                {
                    
                    HttpContext.Response.Cookies.Append("UserRole", "Admin", new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true, 
                        Secure = true 
                    });
                    return RedirectToAction("AdminManagement", "Admin");
                }

                var account = context.SystemAccounts.FirstOrDefault(a => a.AccountEmail == username && a.AccountPassword == password);

                if (account != null)
                {
                    string userRole = account.AccountRole switch
                    {
                        2 => "Lecture",   
                        1 => "Staff"
                    };


                    if (userRole != null)
                    {
                        HttpContext.Response.Cookies.Append("UserId", account.AccountId.ToString(), new CookieOptions
                        {
                            Expires = DateTimeOffset.UtcNow.AddDays(1),
                            HttpOnly = true,
                            Secure = true
                        });

                        HttpContext.Response.Cookies.Append("UserRole", userRole, new CookieOptions
                        {
                            Expires = DateTimeOffset.UtcNow.AddDays(3),
                            HttpOnly = true,
                            Secure = true
                        });

                        if (userRole == "Lecture")
                        {
                            return RedirectToAction("Lecture", "Lecture");
                        }
                        else if (userRole == "Staff")
                        {
                            return RedirectToAction("StaffDashboard", "Staff");
                        }
                    }
                }
                ModelState.AddModelError("", "Incorrect password or email");
            }

            return View(model);
        }



        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
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

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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