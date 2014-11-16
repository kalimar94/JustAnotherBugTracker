using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace BugTrackingSystem.Controllers
{
    public class CommentsController : Controller
    {
        IBugTrackingData unitOfWork;

        public CommentsController(IBugTrackingData unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult View(int id)
        {
            var model = unitOfWork.Comments.Including("User").Where(x => x.IssueId == id);
            ViewBag.IssueId = id;
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
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Users.Single(x => x.Id == userId);

            return PartialView(new IssueComment { User = user });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, IssueComment comment)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var user = unitOfWork.Users.Single(x => x.Id == userId);

                comment.IssueId = id;
                comment.UserId = userId;

                unitOfWork.Comments.Insert(comment);
                unitOfWork.SaveChanges();

                return RedirectToAction("Details", new { id = comment.Id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int issueId, string commentText)
        {
            var comment = unitOfWork.Comments.GetByID(id);
            comment.CommentText = commentText;
            unitOfWork.SaveChanges();

            return RedirectToAction("Details", new { id = comment.Id });
        }

        public ActionResult Edit(int id)
        {
            var model = unitOfWork.Comments.Including("User").Single(x => x.Id == id);
            ViewBag.IssueId = id;
            return PartialView(model);
        }
    }
}