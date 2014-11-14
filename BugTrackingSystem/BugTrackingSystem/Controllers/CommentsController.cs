using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Controllers
{
    public class CommentsController : Controller
    {
        Repository<IssueComment> comments;

        public CommentsController()
        {
            comments = new Repository<IssueComment>(new ApplicationDbContext());
        }

        public ActionResult View(int issueId)
        {
            var model = comments.Where(x => x.IssueId == issueId);
            return View(model);
        }

        public ActionResult Create(int commentId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int commentId, IssueComment newComment)
        {
            return RedirectToAction("View", new { id = commentId });
        }

        public ActionResult Edit(int commentId, FormCollection CommentData)
        {
            var comment = comments.GetByID(commentId);
            return View(comment);
        }

        [HttpPost]
        public ActionResult Edit(int commentId)
        {
            return RedirectToAction("View", new { id = commentId });
        }
    }
}