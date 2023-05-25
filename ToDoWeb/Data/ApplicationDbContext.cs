using Microsoft.EntityFrameworkCore;
using ToDoWeb.Models;

namespace ToDoWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> ToDoList { get; set; }
    }
}

