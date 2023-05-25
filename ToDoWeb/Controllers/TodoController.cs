using Microsoft.AspNetCore.Mvc;
using ToDoWeb.Data;
using ToDoWeb.Models;

namespace ToDoWeb.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TodoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Todolist()
        {
            IEnumerable<Todo> objtasks = _db.ToDoList;
            return View(objtasks);
        }

        //GET
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Todo obj, User user)
        {
            obj.UserId = user.ID;
            _db.ToDoList.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Todolist");
        } 
    }
}
