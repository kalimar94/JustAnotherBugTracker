using BugTrackingSystem.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTrackingSystem.ViewModels
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> AvailableOwners { get; set; }

    }
}