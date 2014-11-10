using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTrackingSystem.Models
{
    public class EditProductViewModel : Product
    {
        public EditProductViewModel() 
        {

        }

        public EditProductViewModel(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.OwnerId = product.OwnerId;
            this.Owner = product.Owner;
            this.Projects = product.Projects;
        }

        public IEnumerable<SelectListItem> AvailableOwners { get; set; }

    }
}