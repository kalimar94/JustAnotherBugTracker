using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repository.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BugTrackingSystem.Models;
using BugTrackingSystem.Data.Repositories;

namespace BugTrackingSystem.Areas.ServiceDesk.Controllers
{
    public class TicketCommentsController : Controller
    {
        private IRepository<TicketComment> comments;

        public TicketCommentsController(IRepository<TicketComment> commentsData)
        {
            this.comments = commentsData;
        }

        public ActionResult View(int id)
        {
            var model = comments.Where(x => x.TicketId == id);
            ViewBag.TicketId = id;
            return PartialView(model);
        }

        public ActionResult Details(int id)
        {
            var model = comments.Single(x => x.Id == id);
            ViewBag.IssueId = id;
            return PartialView(model);
        }

        public ActionResult Create(int id)
        {
            ViewBag.TicketId = id;
            return PartialView(new TicketComment());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, TicketComment comment)
        {
            try
            {
                comment.TicketId = id;
                comments.Insert(comment);
                comments.SaveChanges();

                return RedirectToAction("Details", new { id = comment.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }
    }
}