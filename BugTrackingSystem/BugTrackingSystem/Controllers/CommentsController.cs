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
        CommentUserUnit unitOfWork;

        public CommentsController()
        {
            this.unitOfWork = new CommentUserUnit(new ApplicationDbContext());
        }

        public ActionResult View(int id)
        {
            var model = unitOfWork.Comments.Where(x => x.IssueId == id);
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
        public ActionResult Create(int id, IssueComment newComment)
        {
            return RedirectToAction("View", new { id = id });
        }

        [HttpPost]
        public ActionResult Edit(int commentId, int id, FormCollection CommentData)
        {
            var comment = unitOfWork.Comments.GetByID(commentId);
            return RedirectToAction("View", new { id = id });
        }

        public ActionResult Edit(int id)
        {
            var comment = unitOfWork.Comments.GetByID(id);
            return PartialView(comment);
        }
    }
}