using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Models
{
    public class CreateProjectViewModel : Project
    {

        public IEnumerable<SelectListItem> AvailableManagers { get; set; }

        public IEnumerable<SelectListItem> AvailableProducts { get; set; }

    }
}