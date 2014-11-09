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
            return View(unitOfWork.Projects.GetAll());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            var model = new CreateProjectViewModel()
            {
                AvailableManagers = unitOfWork.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id }).ToList(),
                AvailableProducts = unitOfWork.Projects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id }).ToList()
            };

            return View(model);
        }

        // POST: Projects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Projects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Projects/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
