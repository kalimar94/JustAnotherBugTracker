using AutoMapper;
using BugTrackingSystem.Areas.Administration.ViewModels;
using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Areas.Administration.Controllers
{
    public class AccountAdministrationController : Controller
    {
        private IRepository<User> users;
        private ApplicationUserManager _userManager;

        public AccountAdministrationController(IRepository<User> users)
        {
            this.users = users;
        }

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

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        public ActionResult Index()
        {
            var usersData = users.Including("Roles");
            var viewModel = Mapper.Map<IEnumerable<UserViewModel>>(usersData);

            return View(viewModel);
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var user = users.Including("Roles").SingleOrDefault(x => x.Id == id);

            if (user == null)
            {
                return HttpNotFound("The user was not found");
            }

            var viewModel = Mapper.Map<UserViewModel>(user);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, UserViewModel userData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dbModel = users.Single(x => x.Id == userData.Id);
                    Mapper.Map(userData, dbModel);

                    users.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(userData);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(userData);
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}