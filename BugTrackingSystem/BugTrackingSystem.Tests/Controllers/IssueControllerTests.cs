using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using BugTrackingSystem.Data.Repository.Units;
using BugTrackingSystem.Models;
using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Controllers;
using System.Web.Mvc;
using BugTrackingSystem.Models.Issues;
using BugTrackingSystem.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace BugTrackingSystem.Tests.Controllers
{
    [TestClass]
    public class IssueControllerTests
    {
        private IBugTrackingData dataMock;
        private IssueController sut;

        [TestInitialize]
        public void PrepareMocks()
        {
            dataMock = Mock.Create<IBugTrackingData>();

            Mock.Arrange(() => dataMock.Issues).Returns(Mock.Create<IRepository<Issue>>());
            Mock.Arrange(() => dataMock.Projects).Returns(Mock.Create<IRepository<Project>>());

            sut = new IssueController(dataMock);

        }

        [TestMethod]
        public void DetailsShouldReturnCorrectView()
        {
            // arrange
            int issueId = 15;
            Mock.Arrange(() => dataMock.Issues.GetByID(15)).Returns(new Bug { Name = "IssueName" });

            // act
            var result = sut.Details(Arg.AnyString, issueId) as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Issue));
        }

        [TestMethod]
        public void DetailsShouldRequestCorrectData()
        {
            // arrange
            int issueId = 15;

            // act
            sut.Details(Arg.AnyString, issueId);

            // assert
            Mock.Assert(() => dataMock.Issues.GetByID(Arg.Matches<int>(arg => arg == issueId)));
        }

        [TestMethod]
        public void DetailsShouldReturn404IfIssueDoesNotExist()
        {
            // arrange
            int issueId = 15;
            Mock.Arrange(() => dataMock.Issues.GetByID(15)).Returns(null as Issue);

            // act
            var result = sut.Details(Arg.AnyString, issueId) as HttpNotFoundResult;

            // assert
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("the issue was not found", result.StatusDescription);
        }

        [TestMethod]
        public void CreatePageReturnsCorrectView()
        {
            // arrange
            string projectId = "someProject";
            IEnumerable<Project> fakeProjects = new Project[] { new Project { Id = projectId } };

            Mock.Arrange(() => dataMock.Projects.GetEnumerator()).Returns(fakeProjects.GetEnumerator());

            // act
            var result = sut.Create(projectId) as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(EditIssueViewModel));
        }

        [Ignore] // TODO: correctly stub the form collection
        [TestMethod]
        public void CreatePostInvokesCorrectMethod()
        {
            // arrange
            string projectId = "someProject";

            var formCollection = new FormCollection();
            formCollection.Add("IssueType", "Improvement");
            formCollection.Add("Name", "Improvement");
            formCollection.Add("Summary", "some summary");
            formCollection.Add("controllerContext", "Issue");

            Mock.Arrange(() => dataMock.Issues.Insert(Arg.IsAny<Issue>())).MustBeCalled();

            // act
            var result = sut.Create(projectId, formCollection) as ViewResult;

            // assert
            Mock.Assert(dataMock.Issues);
        }

        [TestMethod]
        public void EditPageReturnsCorrectView()
        {
            // arrange
            int issueId = 15;
            IEnumerable<Issue> fakeIssues = new Issue[] { new Improvement { Id = 15, Assignee = new User() } };

            Mock.Arrange(() => dataMock.Issues.Including("Assignee")).Returns(fakeIssues);

            // act
            var result = sut.Edit(Arg.AnyString, issueId) as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(EditIssueViewModel));
        }
    }
}
