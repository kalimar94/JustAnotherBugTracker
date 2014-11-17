using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Areas.Administration.Controllers
{
    public class IssueAdministrationController : Controller
    {
        private IRepository<Issue> issues;

        public IssueAdministrationController(IRepository<Issue> issueRepository)
        {
            this.issues = issueRepository;
        }

        // GET: Administration/IssueAdministration
        public ActionResult Index()
        {

            return View();
        }
        
        [HttpPost]
        public ActionResult GetData([DataSourceRequest]DataSourceRequest request)
        {

            var issues = this.issues.ToDataSourceResult(request, ModelState);
            return Json(issues);
        }       
    }
}