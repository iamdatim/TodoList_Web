using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoWeb.Data;
using ToDoWeb.Models;

namespace ToDoWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult LoginPage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Query the database to find the user
            var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Authentication successful
                //FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Todolist", "Todo");
            }

            // Invalid credentials
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View("LoginPage");

        }

        //private bool VerifyPasswordHash(string password, string storedHash)
        //{
        //    return BCrypt.Verify(password, storedHash);
        //}
    }


}
