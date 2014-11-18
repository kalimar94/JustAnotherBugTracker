using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using BugTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BugTrackingSystem.Controllers
{

    [Authorize]
    [ValidateInput(false)]
    public class ProjectsController : Controller
    {
        private IBugTrackingData unitOfWork;

        public ProjectsController(IBugTrackingData unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Projects
        public ActionResult Index()
        {
            return View(unitOfWork.Projects.Including("Manager", "Product"));
        }

        // GET: Projects/Details/5
        public ActionResult Details(string id)
        {
            var project = unitOfWork.Projects.Including("Manager", "Issues").SingleOrDefault(x => x.Id == id);

            if (project == null)
            {
                return HttpNotFound("Project was not found");
            }

            return View(project);
        }

        // GET: Projects/Create
         [Authorize(Roles = "admins")]
        public ActionResult Create()
        {
            var model = new EditProjectViewModel()
            {
                AvailableManagers = unitOfWork.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id }).ToList(),
                AvailableProducts = unitOfWork.Products.Select(x => new SelectListItem { Text = x.Name, Value = x.Id }).ToList()
            };

            return View(model);
        }

        // POST: Projects/Create
        [HttpPost]
        [Authorize(Roles = "admins")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditProjectViewModel newProject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Projects.Insert(newProject.Project);
                    unitOfWork.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Projects/Edit/5
         [Authorize(Roles = "admins")]
        public ActionResult Edit(string id)
        {
            var selectedProject = unitOfWork.Projects.GetByID(id);

            if (selectedProject == null)
            {
                return HttpNotFound("Project was not found");
            }

            var viewModel = new EditProjectViewModel
            {
                Project = selectedProject,
                AvailableManagers = unitOfWork.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id }).ToList(),
                AvailableProducts = unitOfWork.Products.Select(x => new SelectListItem { Text = x.Name, Value = x.Id }).ToList()
            };

            return View(viewModel);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [Authorize(Roles = "admins")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, EditProjectViewModel projectData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Projects.Update(projectData.Project);
                    unitOfWork.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(projectData);
            }
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }
    }
}
