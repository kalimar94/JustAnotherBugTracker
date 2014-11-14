using BugTrackingSystem.Models.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Issues
{
    public class NewFeature : Issue
    {
        [DataType(DataType.MultilineText)]
        public string AcceptanceCriteria { get; set; }


        public override IssueType TypeName
        {
            get { return IssueType.Feature; }
        }
    }
}