using BugTrackingSystem.Models.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Issues
{
    public class Improvement : Issue
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string CurrentState { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string AcceptanceCriteria { get; set; }

        public override IssueType TypeName
        {
            get { return IssueType.Improvement; }
        }
    }
}