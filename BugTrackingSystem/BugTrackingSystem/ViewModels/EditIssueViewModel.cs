using BugTrackingSystem.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTrackingSystem.ViewModels
{
    public class EditIssueViewModel
    {
        public Issue IssueData { get; set; }

        public IEnumerable<SelectListItem> IssueTypes { get; set; }

        public IEnumerable<SelectListItem> AvailableUsers { get; set; }

        public IEnumerable<SelectListItem> AvailableProjects { get; set; }

    }
}