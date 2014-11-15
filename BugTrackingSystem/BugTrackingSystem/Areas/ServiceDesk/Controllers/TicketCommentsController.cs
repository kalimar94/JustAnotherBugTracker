using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repository.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Areas.ServiceDesk.Controllers
{
    public class TicketCommentsController : Controller
    {
        TicketCommentUserUnit unitOfWork;

        public TicketCommentsController()
        {
            this.unitOfWork = new TicketCommentUserUnit(new ApplicationDbContext());
        }

        public ActionResult View(int id)
        {
            var model = unitOfWork.Comments.Where(x => x.TicketId == id);
            ViewBag.TicketId = id;
            return PartialView(model);
        }

        public ActionResult Details(int id)
        {
            var model = unitOfWork.Comments.Single(x => x.Id == id);
            ViewBag.IssueId = id;
            return PartialView(model);
        }

        public ActionResult Create(int id)
        {
            ViewBag.TicketId = id;
            return PartialView(new TicketComment());
        }

        [HttpPost]
        public ActionResult Create(int id, TicketComment comment)
        {
            try
            {
                comment.TicketId = id;
                unitOfWork.Comments.Insert(comment);
                unitOfWork.SaveChanges();

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