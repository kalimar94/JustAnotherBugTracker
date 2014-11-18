using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using BugTrackingSystem.ViewModels.Comments;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BugTrackingSystem.Controllers
{
    [ValidateInput(false)]
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
            var model = unitOfWork.Comments.SingleOrDefault(x => x.Id == id);

            if (model == null)
            {
                return HttpNotFound("The comment was not found");
            }

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
        public ActionResult Create(int id, EditCommentViewModel comment)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var user = unitOfWork.Users.Single(x => x.Id == userId);

                var dbModel = new IssueComment
                {
                    IssueId = id,
                    UserId = userId,
                    CommentText = comment.CommentText,
                    CreatedOn = DateTime.Now
                };

                unitOfWork.Comments.Insert(dbModel);
                unitOfWork.SaveChanges();

                return RedirectToAction("Details", new { id = dbModel.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(comment);
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