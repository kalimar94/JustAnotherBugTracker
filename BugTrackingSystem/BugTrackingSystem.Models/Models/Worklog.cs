using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models
{
    public class Worklog
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int IssueId { get; set; }

        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }
    }
}
