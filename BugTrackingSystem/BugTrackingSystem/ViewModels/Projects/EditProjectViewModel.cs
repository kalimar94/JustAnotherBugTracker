using BugTrackingSystem.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTrackingSystem.ViewModels
{
    public class EditProjectViewModel
    {
        public Project Project { get; set; }

        public IEnumerable<SelectListItem> AvailableManagers { get; set; }

        public IEnumerable<SelectListItem> AvailableProducts { get; set; }
    }
}