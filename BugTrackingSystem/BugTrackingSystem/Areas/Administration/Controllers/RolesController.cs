using BugTrackingSystem.Areas.Administration.ViewModels;
using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace BugTrackingSystem.Areas.Administration.Controllers
{
    public class RolesController : Controller
    {
        private IBugTrackingData unitOfWork;

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public RolesController(IBugTrackingData unitOfwork)
	    {
            this.unitOfWork = unitOfwork;
	    }

        // GET: Administration/Roles
        public ActionResult Index()
        {
            return View(unitOfWork.Roles);
        }

        public ActionResult Details(string id)
        {

            var viewModel = new UserRoleViewModel
            {
                AvailableUsers = unitOfWork.Users.Select(x=> new SelectListItem{ Text = x.UserName, Value = x.Id}),
                Role = unitOfWork.Roles.GetByID(id),
                NewUser = new User(),
                UsersInRole = unitOfWork.Users.Including("Roles").Where(user => user.Roles.Any(role => role.RoleId == id))
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRole(UserRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserManager.AddToRoleAsync(viewModel.NewUser.Id, viewModel.Role.Name);
            }

            return RedirectToAction("Index");
        }
    }
}