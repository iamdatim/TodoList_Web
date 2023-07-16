using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using List.Data;
using List.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using List.Repository.Interface;

namespace List.Services
{
    public class UserTodoManagement : IUserTodoManagement
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public UserTodoManagement(ApplicationDbContext context, 
                                 UserManager<IdentityUser> userManager, 
                                 SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<List<Todo>> GetTasksFromDataSource(ClaimsPrincipal User)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var signedInUser = await _userManager.GetUserAsync(User);
                string userId = signedInUser.Id;

                List<Todo> todoItems = await _context.ToDoList
                    .Where(todo => todo.UserId == userId)
                    .ToListAsync();

                return todoItems;
            }

            return await _context.ToDoList.ToListAsync();
        }


        public async Task<bool> TodoExists(Guid id, ClaimsPrincipal user)
        {
            if (_signInManager.IsSignedIn(user))
            {
                var signedInUser = await _userManager.GetUserAsync(user);
                string userId = signedInUser.Id;

                var todoExists = await _context.ToDoList
                    .AnyAsync(todo => todo.UserId == userId && todo.Id == id);

                return todoExists;
            }

            return false;
        }

    }
}
