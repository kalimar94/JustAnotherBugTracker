using BugTrackingSystem.Models.Enums;
using BugTrackingSystem.Models.Models.Enums;
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

        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        public string AssigneeId { get; set; }

        [ForeignKey("AssigneeId")]
        public User Assignee { get; set; }

        public string ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [NotMapped]
        public abstract IssueType TypeName { get; }
    }
}