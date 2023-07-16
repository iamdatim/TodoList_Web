using List.Data;
using List.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace List.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Index(string filter, string sortBy)
        {
            // Retrieve tasks from the database or any other data source
            var tasks = GetTasksFromDataSource();

            // Apply filtering based on the filter parameter
            if (!string.IsNullOrEmpty(filter))
            {
                tasks = tasks.Where(t => t.Title.Contains(filter));
            }

            // Apply sorting based on the sortBy parameter
            switch (sortBy)
            {
                case "DueDate":
                    tasks = tasks.OrderBy(t => t.Duedate);
                    break;
                case "Priority":
                    tasks = tasks.OrderBy(t => t.PriorityLevel);
                    break;
                case "IsComplete":
                    tasks = tasks.OrderBy(t => t.IsComplete);
                    break;
            }

            return View(tasks);
        }

        private IQueryable<Todo> GetTasksFromDataSource()
        {
            return _context.ToDoList;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}