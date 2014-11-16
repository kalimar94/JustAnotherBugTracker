using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BugTrackingSystem.Controllers
{
    [Authorize]
    public class WorkLogController : Controller
    {
        private IBugTrackingData unitOfWork;

        public WorkLogController(IBugTrackingData unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult View(int id)
        {
            var model = unitOfWork.WorkLogs.Including("User").Where(x => x.IssueId == id);
            ViewBag.IssueId = id;
            return PartialView(model);
        }

        public ActionResult Details(int id)
        {
            var model = unitOfWork.WorkLogs.Single(x => x.Id == id);
            ViewBag.IssueId = id;
            return PartialView(model);
        }

        public ActionResult Create(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Users.Single(x => x.Id == userId);

            return PartialView(new Worklog { User = user });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Worklog log)
        {
            try
            {
                if (ModelState.IsValidField("Date") && ModelState.IsValidField("Description")
                    && ModelState.IsValidField("HoursWorked"))
                {
                    var userId = User.Identity.GetUserId();

                    log.IssueId = id;
                    log.UserId = userId;

                    unitOfWork.WorkLogs.Insert(log);
                    unitOfWork.SaveChanges();

                    return RedirectToAction("Details", new { id = log.Id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid model data");
                    return View(log);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }
    }
}