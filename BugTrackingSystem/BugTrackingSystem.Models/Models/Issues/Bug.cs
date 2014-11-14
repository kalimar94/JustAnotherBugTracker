namespace BugTrackingSystem.Models.Issues
{
    using BugTrackingSystem.Models.Enums;
    using BugTrackingSystem.Models.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class Bug : Issue
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string ExpectedResult { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string StepsToReproduce { get; set; }

        public Severity Severity { get; set; }

        [DataType(DataType.MultilineText)]
        public string Resolution { get; set; }

        public override IssueType TypeName
        {
            get { return IssueType.Bug; }
        }
    }
}