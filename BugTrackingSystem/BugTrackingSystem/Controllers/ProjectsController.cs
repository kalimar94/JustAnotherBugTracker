using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repositories.Units;
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
        private ProductProjectUserUnit unitOfWork;

        public ProjectsController()
        {
            unitOfWork = new ProductProjectUserUnit(new ApplicationDbContext());
        }

        // GET: Projects
        public ActionResult Index()
        {
            return View(unitOfWork.Projects.Including("Manager", "Product"));
        }

        // GET: Projects/Details/5
        public ActionResult Details(string id)
        {
            var project = unitOfWork.Projects.Including("Manager", "Issues").Single(x => x.Id == id);
            return View(project);
        }

        // GET: Projects/Create
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
        public ActionResult Edit(string id)
        {
            var selectedProject = unitOfWork.Projects.Single(x => x.Id == id);

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
            catch
            {
                return View();
            }
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // DELETE: Projects/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                unitOfWork.Projects.Delete(id);
                unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
