using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace List.Models
{
    public class Todo
    {
        public Guid Id = Guid.NewGuid();
        [Key]
        public int ListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime Duedate { get; set; }
        public string PriorityLevel { get; set; }
        public bool IsComplete { get; set; }
        //public Guid UserId { get; set; }
    }
}
