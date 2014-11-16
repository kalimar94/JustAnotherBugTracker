using BugTrackingSystem.Models.Models.Enums;
using System.ComponentModel.DataAnnotations;
namespace BugTrackingSystem.Models.Issues
{
    public class Task : Issue
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string AcceptanceCriteria { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DataType DueDate { get; set; }

        public override IssueType TypeName
        {
            get { return IssueType.Task; }
        }
    }
}
