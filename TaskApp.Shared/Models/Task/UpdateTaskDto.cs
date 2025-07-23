using System.ComponentModel.DataAnnotations;
using TaskApp.Shared.Enums;

namespace TaskApp.Shared.Models.Task
{
    public class UpdateTaskDto
    {
        [Required]
        public  Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }
    }
}
