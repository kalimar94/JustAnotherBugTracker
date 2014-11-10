using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Models.Issues
{
    public class Improvement : Issue
    {
        public string CurrentState { get; set; }

        public string AcceptanceCriteria { get; set; }

    }
}