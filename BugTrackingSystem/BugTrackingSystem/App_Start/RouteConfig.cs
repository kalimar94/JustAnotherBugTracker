using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BugTrackingSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "IssueRoute",
                url: "Projects/{projectId}/{action}/{issueId}",
                defaults: new { controller = "Issue", action = "Details" }
            );

            routes.MapRoute(
                name: "IssueCreate",
                url: "Projects/{projectId}/CreateIssue",
                defaults: new { controller = "Issue", action = "Create" }
            );

            routes.MapRoute(
                name: "Index",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
