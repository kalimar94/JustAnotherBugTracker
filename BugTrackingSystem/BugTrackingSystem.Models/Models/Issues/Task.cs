using BugTrackingSystem.Models.Models.Enums;
namespace BugTrackingSystem.Models.Issues
{
    public class Task : Issue
    {
        public override IssueType TypeName
        {
            get { return IssueType.Task; }
        }
    }
}
