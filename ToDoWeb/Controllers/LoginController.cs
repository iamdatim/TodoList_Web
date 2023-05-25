using Microsoft.AspNetCore.Mvc;
using ToDoWeb.Data;

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

            if (user != null && VerifyPasswordHash(password, user.Password))
            {
                // Authentication successful
                //FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Todo", "Todolist");
            }

            // Invalid credentials
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();

        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
