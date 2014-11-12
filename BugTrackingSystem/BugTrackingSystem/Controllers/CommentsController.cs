using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public ActionResult View(int issueId)
        {
            // get comments for given issue

            return View();
        }

        public JsonResult Edit(int commentId, FormCollection CommentData)
        {
            return Json("data");
        }
    }
}