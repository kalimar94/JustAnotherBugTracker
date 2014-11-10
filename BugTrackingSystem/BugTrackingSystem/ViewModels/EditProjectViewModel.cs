using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTrackingSystem.Models
{
    public class EditProjectViewModel : Project
    {
        public EditProjectViewModel()
        {

        }

        public EditProjectViewModel(Project project)
        {
            this.Id = project.Id;
            this.Name = project.Name;
            this.Issues = project.Issues;
            this.ProductId = project.ProductId;
            this.Product = project.Product;
            this.Manager = project.Manager;
            this.ManagerId = project.ManagerId;
        }

        public IEnumerable<SelectListItem> AvailableManagers { get; set; }

        public IEnumerable<SelectListItem> AvailableProducts { get; set; }

    }
}