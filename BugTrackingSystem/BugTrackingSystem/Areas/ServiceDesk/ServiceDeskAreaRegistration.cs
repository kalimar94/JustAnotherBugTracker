using System.Web.Mvc;

namespace BugTrackingSystem.Areas.ServiceDesk
{
    public class ServiceDeskAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ServiceDesk";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ServiceDesk_default",
                "ServiceDesk/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}