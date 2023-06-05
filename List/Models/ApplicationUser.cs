using Microsoft.AspNetCore.Identity;

namespace List.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
       // public List<Todo> Todos { get; set; } 
    }
}
