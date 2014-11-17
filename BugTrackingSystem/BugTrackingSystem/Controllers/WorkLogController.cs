using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BugTrackingSystem.ViewModels.Worklog;

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

            return PartialView(new CreateWorklogViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CreateWorklogViewModel model)
        {
            try
            {
                if (ModelState.IsValidField("Date") && ModelState.IsValidField("Description")
                    && ModelState.IsValidField("HoursWorked"))
                {
                    var userId = User.Identity.GetUserId();

                    var log = new Worklog
                    {
                        Date = model.Date,
                        HoursWorked = model.HoursWorked,
                        Description = model.Description,
                        UserId = userId,
                        IssueId = id
                    };

                    unitOfWork.WorkLogs.Insert(log);
                    unitOfWork.SaveChanges();

                    return RedirectToAction("Details", new { id = log.Id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid model data");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
    }
}