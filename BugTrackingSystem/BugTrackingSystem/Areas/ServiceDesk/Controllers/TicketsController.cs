using BugTrackingSystem.Areas.ServiceDesk.ViewModels;
using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repositories;
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
        Repository<Ticket> ticketRepo;
        Repository<User> usersRepo;

        public TicketsController()
        {
            var context = new ApplicationDbContext();
            ticketRepo = new Repository<Ticket>(context);
            usersRepo = new Repository<User>(context);
        }

        // GET: ServiceDesk/Ticket
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiceDesk/Ticket/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServiceDesk/Ticket/Create
        public ActionResult Create()
        {
            var viewModel = new CreateTicketViewModel
            {
                AvailableUsers = usersRepo.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id})
            };

            return View();
        }

        // POST: ServiceDesk/Ticket/Create
        [HttpPost]
        public ActionResult Create(CreateTicketViewModel newTicket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                               
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
