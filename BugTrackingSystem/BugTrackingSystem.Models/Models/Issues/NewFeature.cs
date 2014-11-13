using BugTrackingSystem.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Models.Issues
{
    public class NewFeature : Issue
    {
        public string AcceptanceCriteria { get; set; }


        public override IssueType TypeName
        {
            get { return IssueType.Feature; }
        }
    }
}