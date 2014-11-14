using BugTrackingSystem.Areas.ServiceDesk.ViewModels;
using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using BugTrackingSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Areas.ServiceDesk.Controllers
{
    public class TicketsController : Controller
    {
        ProductTicketUnit unitOfWork;

        public TicketsController()
        {
            this.unitOfWork = new ProductTicketUnit(new ApplicationDbContext());

        }

        // GET: ServiceDesk/Ticket
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiceDesk/Ticket/Details/5
        public ActionResult Details(int id)
        {
            var ticket = unitOfWork.Tickets.GetAll("RelatedProduct").Single(x => x.Id == id);

            return View(ticket);
        }

        // GET: ServiceDesk/Ticket/Create
        public ActionResult Create()
        {
            var viewModel = new CreateTicketViewModel
            {
                AvailableProducts = unitOfWork.Products.Select(x => new SelectListItem { Text = x.Name, Value = x.Id })
            };

            return View(viewModel);
        }

        // POST: ServiceDesk/Ticket/Create
        [HttpPost]
        public ActionResult Create(CreateTicketViewModel newTicket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Tickets.Insert(newTicket.Ticket);
                    unitOfWork.SaveChanges();
                }

                return RedirectToAction("Details", new { id = newTicket.Ticket.Id });
            }
            catch
            {
                return View();
            }
        }

    }
}
