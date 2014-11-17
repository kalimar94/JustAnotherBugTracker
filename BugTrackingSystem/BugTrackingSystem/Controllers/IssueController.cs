using BugTrackingSystem.Data;
using BugTrackingSystem.Data.Repository.Units;
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
        private IBugTrackingData unitOfWork;

        public IssueController(IBugTrackingData unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Details(string projectId, int issueId)
        {
            var issue = unitOfWork.Issues.GetByID(issueId);

            if (issue == null)
            {
                return HttpNotFound("the issue was not found");
            }

            return View(issue);
        }

        // GET: Issue/Create
        public ActionResult Create(string projectId)
        {
            var viewModel = PrepareViewModel(null);
            viewModel.AvailableProjects.Single(x => x.Value == projectId).Selected = true;

            return View(viewModel);
        }

        // POST: Issue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string projectId, FormCollection collection)
        {
            try
            {
                var issue = ParseForm(collection);
                unitOfWork.Issues.Insert(issue);

                unitOfWork.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = projectId });
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
            var issue = unitOfWork.Issues.Including("Assignee").Single(x => x.Id == issueId);
            var viewModel = PrepareViewModel(issue);

            return View(viewModel);
        }

        // POST: Issue/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string projectId, int issueId, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var issue = ParseForm(collection);
                    issue.Id = issueId;
                    unitOfWork.Issues.Update(issue);
                    unitOfWork.SaveChanges();
                }

                return RedirectToAction("Details", new {issueId = issueId, projectId = projectId});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(collection);
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

        private EditIssueViewModel PrepareViewModel(Issue issue)
        {
            var viewModel = new EditIssueViewModel
            {
                AvailableUsers = unitOfWork.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id }),
                AvailableProjects = unitOfWork.Projects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id }),
                IssueTypes = from IssueType e in Enum.GetValues(typeof(IssueType))
                             select new SelectListItem { Text = e.ToString() },
                IssueData = issue
            };
            return viewModel;
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
