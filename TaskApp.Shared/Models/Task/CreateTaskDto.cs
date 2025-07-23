using System.ComponentModel.DataAnnotations;
using TaskApp.Shared.Enums;

namespace TaskApp.Shared.Models.Task
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;


        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required]
        [EnumDataType(typeof(Status), ErrorMessage = "Invalid status value.")]
        public Status Status { get; set; }

        [Required]
        [EnumDataType(typeof(Priority), ErrorMessage = "Invalid priority value.")]
        public Priority Priority { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }
    }
}
