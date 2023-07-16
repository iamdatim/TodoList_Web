using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace List.Models
{

    
    public class Todo
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Title is required")]
        [MaxLength (20, ErrorMessage ="Title lenght must not be more than 20 Characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Due date is required")]
        public DateTime Duedate { get; set; }

        [Required(ErrorMessage = "Priority level is required.")]
        [RegularExpression(@"^(Low|Medium|High|low|medium|high)$", ErrorMessage = "Invalid priority level, priority level must be either Low, Medium or High.")]
        public string PriorityLevel { get; set; }
        public bool IsComplete { get; set; }
        public IdentityUser? User { get; set; }

        [ForeignKey("UserId")]
        public string? UserId { get; set; }
    }
}
