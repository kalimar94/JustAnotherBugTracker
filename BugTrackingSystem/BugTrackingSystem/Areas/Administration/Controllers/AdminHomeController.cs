using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Areas.Administration.Controllers
{
    [Authorize(Roles = "admins")]
    public class AdminHomeController : Controller
    {
        // GET: Administration/AdminHome
        public ActionResult Index()
        {
            return View();
        }
    }
}