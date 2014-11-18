using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using BugTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Controllers
{
    [Authorize]
    [ValidateInput(false)]
    public class ProductsController : Controller
    {
        private IBugTrackingData unitOfWork;

        public ProductsController(IBugTrackingData unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }

        // GET: Product
        public ActionResult Index()
        {
            return View(unitOfWork.Products.Including("Owner"));
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {
            return View(model:id);
        }

        [OutputCache(VaryByParam="id", Duration=5 * 60)]
        [ChildActionOnly]
        public ActionResult DetailsData(string id)
        {
            var model = unitOfWork.Products.Including("Owner").Single(x => x.Id == id);
            return View(model);
        }

        [Authorize(Roles = "admins")]
        // GET: Product/Create
        public ActionResult Create()
        {
            var viewModel = new EditProductViewModel
            {
                AvailableOwners = unitOfWork.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id })
            };

            return View(viewModel);
        }

        // POST: Product/Create
        [HttpPost]
        [Authorize(Roles = "admins")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditProductViewModel newProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Products.Insert(newProduct.Product);
                    unitOfWork.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "admins")]
        public ActionResult Edit(string id)
        {
            var selectedProduct = unitOfWork.Products.Single(x => x.Id == id);

            var viewModel = new EditProductViewModel
            {
                Product = selectedProduct,
                AvailableOwners = unitOfWork.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id })
            };

            return View(viewModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [Authorize(Roles = "admins")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, EditProductViewModel editedProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Products.Update(editedProduct.Product);
                    unitOfWork.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(editedProduct);
            }
        }

    }
}
