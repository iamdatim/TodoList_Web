using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
           // obj.UserId = user.ID;
            _db.ToDoList.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Todolist");
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _db.ToDoList == null)
            {
                return NotFound();
            }

            var todo = await _db.ToDoList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_db.ToDoList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ToDoList'  is null.");
            }
            var todo = await _db.ToDoList.FindAsync(id);
            if (todo != null)
            {
                _db.ToDoList.Remove(todo);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Todolist));
        }
    }
}
