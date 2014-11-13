using BugTrackingSystem.Models.Models.Enums;

namespace BugTrackingSystem.Models.Issues
{
    public class Story : Issue
    {
        public override IssueType TypeName
        {
            get { return IssueType.Story; }
        }
    }
}