using System.ComponentModel.DataAnnotations;
using TaskApp.Shared.Enums;

namespace TaskApp.Shared.Models.Task
{
    public class TaskDto
    {

        public Guid Id { get; set; }


        public string Title { get; set; } 


        public string? Description { get; set; }


        public Status Status { get; set; }

        public string StatusDisplay => Status.ToString();


        public Priority Priority { get; set; }

        public string PriorityDisplay => Priority.ToString();

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
