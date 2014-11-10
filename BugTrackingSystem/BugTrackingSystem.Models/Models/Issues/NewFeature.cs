using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Models.Issues
{
    public class NewFeature : Issue
    {
        public string AcceptanceCriteria { get; set; }

    }
}