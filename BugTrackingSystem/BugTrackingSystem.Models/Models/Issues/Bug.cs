namespace BugTrackingSystem.Models.Issues
{
    using BugTrackingSystem.Models.Enums;
    using BugTrackingSystem.Models.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class Bug : Issue
    {
        [Required]
        public string ExpectedResult { get; set; }

        public string StepsToReproduce { get; set; }

        public Severity Severity { get; set; }

        public string Resolution { get; set; }

        public override IssueType TypeName
        {
            get { return IssueType.Bug; }
        }
    }
}