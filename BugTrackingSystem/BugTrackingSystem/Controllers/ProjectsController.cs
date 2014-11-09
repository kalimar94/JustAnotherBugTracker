using BugTrackingSystem.Models;
using BugTrackingSystem.Models.Repositories;
using BugTrackingSystem.Models.Repositories.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BugTrackingSystem.Controllers
{
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
            return View(unitOfWork.Projects.GetAll("Manager", "Product"));
        }

        // GET: Projects/Details/5
        public ActionResult Details(string id)
        {
            return View(unitOfWork.Projects.GetByID(id));
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
                    unitOfWork.Projects.Insert(newProject);
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
            var selectedProduct = unitOfWork.Projects.Single(x => x.Id == id);

            var viewModel = new EditProjectViewModel(selectedProduct)
            {
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
                var toUpdate = unitOfWork.Projects.GetByID(id);
                TryUpdateModel(toUpdate);
                unitOfWork.SaveChanges();

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
