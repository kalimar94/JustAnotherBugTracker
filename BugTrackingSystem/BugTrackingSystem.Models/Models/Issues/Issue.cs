using BugTrackingSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models
{
    public abstract class Issue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Priority Priority { get; set; }

        public string Summary { get; set; }

        public string AssigneeId { get; set; }

        [ForeignKey("AssigneeId")]
        public User Assignee { get; set; }

        public virtual Project Project { get; set; }
    }
}