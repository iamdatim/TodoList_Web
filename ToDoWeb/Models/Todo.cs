using System.ComponentModel.DataAnnotations;

namespace ToDoWeb.Models
{
    public class Todo
    {
        [Key]
        public int TaskId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public string PriorityLevel { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public Guid UserId { get; set; }
    }
}
