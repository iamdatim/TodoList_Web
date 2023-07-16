using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using List.Data;
using List.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using List.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace List.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserTodoManagement _usertodomanagement;


        public TodoController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserTodoManagement usertodomanagement)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _usertodomanagement = usertodomanagement;

        }

        //GET: Todo
        public async Task<IActionResult> Index()
        {
            var tasks = await _usertodomanagement.GetTasksFromDataSource(User);
            return View(tasks);
        }


        //Getting Filter Result In Another Page
        public async Task<IActionResult> FilteredTasks(string filter, string sortBy)
        {

            var tasks = await _usertodomanagement.GetTasksFromDataSource(User);

            // Apply filtering based on the filter parameter
            //if (!string.IsNullOrEmpty(filter))
            //{
            //    tasks = tasks.Where(t => t.Title.Contains(filter)).ToList();
            //}

            if (filter == "PriorityLevel")
            {
                tasks = tasks.Where(t => t.PriorityLevel == "high" || t.PriorityLevel == "High").ToList();
            }

            if (filter == "IsComplete")
            {
                tasks = tasks.Where(t => t.IsComplete == true).ToList();
            }

            // Apply sorting based on the sortBy parameter
            switch (sortBy)
            {
                case "DueDate":
                    tasks = tasks.OrderBy(t => t.Duedate).ToList();
                    break;
                case "Priority":
                    tasks = tasks.OrderBy(t => t.PriorityLevel).ToList();
                    break;
                case "IsComplete":
                    tasks = tasks.OrderBy(t => t.IsComplete).ToList();
                    break;

            }



            if (tasks.Count() == 0)
            {
                ViewData["ErrorMessage"] = "No tasks found.";
                return View("Error");
            }


            return View("FilteredTasks", tasks);
        }


        // GET: Todo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ToDoList == null)
            {
                return NotFound();
            }

            var todo = await _context.ToDoList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Todo Addtodo)
        {



            if (_signInManager.IsSignedIn(User))
            {
                var SignedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Addtodo.UserId = SignedInUser;


                if (SignedInUser == Addtodo.UserId)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(Addtodo);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }




            foreach (var modelStateEntry in ModelState.Values)
            {
                foreach (var error in modelStateEntry.Errors)
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }



            return View(Addtodo);
        }

        //// GET: Todo/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.ToDoList == null)
        //    {
        //        return NotFound();
        //    }

        //    var todo = await _context.ToDoList.FindAsync(id);
        //    if (todo == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(todo);
        //}

        ////POST: Todo/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("ListId,Title,Description,CreatedDate,Duedate,PriorityLevel,IsComplete")] Todo todo)
        //{
        //    if (id != todo.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(todo);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (! await _usertodomanagement.TodoExists(todo.Id, User))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(todo);
        //}




        // GET: Todoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ToDoList == null)
            {
                return NotFound();
            }

            var todo = await _context.ToDoList.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        // POST: Todoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Title, Description, Duedate, PriorityLevel, IsComplete")] Todo todo)
        {

            if (id != todo.Id)
            {
                return NotFound();
            }

            var SignedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            todo.UserId = SignedInUser;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _usertodomanagement.TodoExists(todo.Id, User))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        private bool TodoExists(Guid id)
        {
            return (_context.ToDoList?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //[HttpPut]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid? id, [Bind("Id, Title, Description, CreatedDate, DueDate, PriorityLevel, IsComplete")] Todo todo)
        //{
        //    if (id != todo.Id)
        //    {
        //        return NotFound();
        //    }

        //    //if (ModelState.IsValid)
        //    //{
        //    try
        //    {
        //        var existingTodo = await _context.ToDoList.FindAsync(id);
        //        if (existingTodo == null)
        //        {
        //            return NotFound();
        //        }

        //        existingTodo.Title = todo.Title;
        //        existingTodo.Description = todo.Description;
        //        existingTodo.CreatedDate = todo.CreatedDate;
        //        existingTodo.Duedate = todo.Duedate;
        //        existingTodo.PriorityLevel = todo.PriorityLevel;
        //        existingTodo.IsComplete = todo.IsComplete;

        //        _context.Update(existingTodo);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await _usertodomanagement.TodoExists(todo.Id, User))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    //}

        //    return View(todo);
        //    foreach (var modelStateEntry in ModelState.Values)
        //    {
        //        foreach (var error in modelStateEntry.Errors)
        //        {
        //            Console.WriteLine($"Validation Error: {error.ErrorMessage}");
        //        }
        //    }

        //}



        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ToDoList == null)
            {
                return NotFound();
            }

            var todo = await _context.ToDoList
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
            if (_context.ToDoList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ToDoList'  is null.");
            }
            var todo = await _context.ToDoList.FindAsync(id);
            if (todo != null)
            {
                _context.ToDoList.Remove(todo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool TodoExists(Guid id)
        //{
        //    return (_context.ToDoList?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

    }
}
