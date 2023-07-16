using List.Models;
using System.Security.Claims;

namespace List.Repository.Interface
{
    public interface IUserTodoManagement
    {
        Task<List<Todo>> GetTasksFromDataSource(ClaimsPrincipal User);
        Task<bool> TodoExists(Guid id, ClaimsPrincipal user);
    }
}
