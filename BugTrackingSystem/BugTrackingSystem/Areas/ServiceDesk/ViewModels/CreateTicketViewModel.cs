using BugTrackingSystem.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTrackingSystem.Areas.ServiceDesk.ViewModels
{
    public class CreateTicketViewModel
    {
        public Ticket Ticket { get; set; }

        public IEnumerable<SelectListItem> AvailableProducts { get; set; }
    }
}