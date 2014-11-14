using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repositories.Units;
using BugTrackingSystem.Models;
using BugTrackingSystem.Models.Issues;
using BugTrackingSystem.Models.Models.Enums;
using BugTrackingSystem.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BugTrackingSystem.Controllers
{
    public class IssueController : Controller
    {
        private IssueProjectUserUnit unitOfWork;

        public IssueController()
        {
            unitOfWork = new IssueProjectUserUnit(new ApplicationDbContext());
        }

        public ActionResult Details(string projectId, int issueId)
        {
            var issue = unitOfWork.Issues.GetByID(issueId);
            return View(issue);
        }

        // GET: Issue/Create
        public ActionResult Create(string projectId)
        {
            var viewModel = new EditIssueViewModel()
            {
                AvailableUsers = unitOfWork.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id }),
                AvailableProjects = unitOfWork.Projects.Select(x=> new SelectListItem { Text = x.Name, Value = x.Id }),
                IssueTypes = from IssueType e in Enum.GetValues(typeof(IssueType))
                             select new SelectListItem { Text = e.ToString() }
            };

            viewModel.AvailableProjects.Single(x => x.Value == projectId).Selected = true;

            return View(viewModel);
        }


        // POST: Issue/Create
        [HttpPost]
        public ActionResult Create(string projectId, FormCollection collection)
        {
            try
            {
                var issue = ParseForm(collection);

                unitOfWork.Issues.Insert(issue);

                unitOfWork.SaveChanges();
                return RedirectToAction("Details", "Project", new { id = projectId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Create(projectId);
            }
        }

        // GET: Issue/Edit/5
        public ActionResult Edit(string projectId, int issueId)
        { 
            return View();
        }

        // POST: Issue/Edit/5
        [HttpPost]
        public ActionResult Edit(int issueId, FormCollection collection)
        {
            try
            {
                var viewModel = ParseForm(collection);
                var issue = unitOfWork.Issues.Single(x => x.Id == issueId);

                unitOfWork.Issues.Update(issue);

                unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                return View();
            }
        }

        // POST: Issue/Delete/5
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

        [HttpPost]
        public ActionResult GetEditDataFor(string data)
        {
            IssueType type = (IssueType)Enum.Parse(typeof(IssueType), data);

            switch (type)
            {
                case IssueType.Bug:
                    return PartialView(@"Partials\BugEditor", new Bug());
                case IssueType.Task:
                    return PartialView(@"Partials\BugEditor", new Bug());
                case IssueType.Story:
                    return PartialView(@"Partials\BugEditor", new Bug());
                case IssueType.Feature:
                    return PartialView(@"Partials\FeatureEditor", new NewFeature());
                case IssueType.Improvement:
                    return PartialView(@"Partials\ImprovementEditor", new Improvement());
                default:
                    throw new ArgumentException();
            }
        }

        private Issue ParseForm(FormCollection formData)
        {
            var viewModel = new EditIssueViewModel { IssueData = new Task() };
            TryUpdateModel(viewModel);

            var issue = GetAdditionalIssueData(formData);
            issue.Name = viewModel.IssueData.Name;
            issue.Priority = viewModel.IssueData.Priority;
            issue.Summary = viewModel.IssueData.Summary;
            issue.AssigneeId = viewModel.IssueData.AssigneeId;

            return issue;
        }

        private Issue GetAdditionalIssueData(FormCollection formData)
        {
            var issueType = (IssueType)Enum.Parse(typeof(IssueType), formData["IssueType"]);

            switch (issueType)
            {
                case IssueType.Bug:
                    Bug newBug = new Bug();
                    TryUpdateModel<Bug>(newBug);
                    return newBug;

                case IssueType.Task:
                    Task newTask = new Task();
                    TryUpdateModel<Task>(newTask);
                    return newTask;

                case IssueType.Story:
                    Story newStory = new Story();
                    TryUpdateModel<Story>(newStory);
                    return newStory;

                case IssueType.Feature:
                    NewFeature newFeature = new NewFeature();
                    TryUpdateModel<NewFeature>(newFeature);
                    return newFeature;

                case IssueType.Improvement:
                    Improvement newImprovement = new Improvement();
                    TryUpdateModel<Improvement>(newImprovement);
                    return newImprovement;
                default:
                    throw new ArgumentException();
            }
        }

    }
}
