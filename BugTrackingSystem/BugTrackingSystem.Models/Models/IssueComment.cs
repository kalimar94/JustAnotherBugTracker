using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models
{
    public class IssueComment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string CommentText { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int IssueId { get; set; }

        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
