using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Areas.ServiceDesk.Controllers
{
    public class ServiceDeskHomeController : Controller
    {
        // GET: ServiceDesk/SevriceDeskHome
        public ActionResult Index()
        {
            return View();
        }
    }
}