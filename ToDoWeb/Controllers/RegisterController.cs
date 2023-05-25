using Microsoft.AspNetCore.Mvc;
using ToDoWeb.Data;
using ToDoWeb.Models;

namespace ToDoWeb.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RegisterController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Todolist()
        {
            IEnumerable<User> objtasks = _db.Users;
            return View(objtasks);
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(User user)
        {
            //if (ModelState.IsValid)
            //{
                _db.Users.Add(user);
                _db.SaveChanges();
            TempData["SuccessMessage"] = "Registration successful! Welcome to our website.";
            return RedirectToAction("LoginPage", "Login");
            //}

            // If registration data is not valid, return the registration view with validation errors
           // return View(user);
        }
    }
}
