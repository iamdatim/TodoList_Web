using System.ComponentModel.DataAnnotations;

namespace ToDoWeb.Models
{
    public class User
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
        [Required]
        public string Fullname { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public List<Todo> ToDoList { get; set; } = new List<Todo>();
    }
}
