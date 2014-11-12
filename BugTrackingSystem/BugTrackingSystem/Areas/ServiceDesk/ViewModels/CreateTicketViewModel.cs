using BugTrackingSystem.Models.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTrackingSystem.Areas.ServiceDesk.ViewModels
{
    public class CreateTicketViewModel : Ticket
    {
        public IEnumerable<SelectListItem> AvailableUsers { get; set; }

    }
}