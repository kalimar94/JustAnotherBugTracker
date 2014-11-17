using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugTrackingSystem.Data.Repository.Units;
using Telerik.JustMock;
using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;
using BugTrackingSystem.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using BugTrackingSystem.ViewModels;

namespace BugTrackingSystem.Tests.Controllers
{
    [TestClass]
    public class ProjectsControllerTests
    {
        private IBugTrackingData dataMock;
        private ProjectsController sut;

        [TestInitialize]
        public void PrepareMocks()
        {
            dataMock = Mock.Create<IBugTrackingData>();

            Mock.Arrange(() => dataMock.Products).Returns(Mock.Create<IRepository<Product>>());
            Mock.Arrange(() => dataMock.Projects).Returns(Mock.Create<IRepository<Project>>());

            sut = new ProjectsController(dataMock);

        }

        [TestMethod]
        public void DetailsShouldReturnCorrectView()
        {
            // arrange
            string projectId = "someProject";
            IEnumerable<Project> fakeProjects = new Project[] { new Project { Id = projectId } };

            Mock.Arrange(() => dataMock.Projects.Including("Manager", "Issues")).Returns(fakeProjects);

            // act
            var result = sut.Details(projectId) as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Project));
        }

        [TestMethod]
        public void DetailsShouldReturn404IfIssueDoesNotExist()
        {
            // arrange
            string projectId = "someProject";

            // act
            var result = sut.Details(projectId) as HttpNotFoundResult;

            // assert
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("Project was not found", result.StatusDescription);
        }

        [TestMethod]
        public void CreatePageReturnsCorrectView()
        {
            // arrange
            string projectId = "someProject";
            IEnumerable<Project> fakeProjects = new Project[] { new Project { Id = projectId } };

            Mock.Arrange(() => dataMock.Projects.GetEnumerator()).Returns(fakeProjects.GetEnumerator());

            // act
            var result = sut.Create() as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(EditProjectViewModel));
        }

        [TestMethod]
        public void CreatePostInvokesCorrectMethod()
        {
            // arrange
            Mock.Arrange(() => dataMock.Projects.Insert(Arg.IsAny<Project>())).MustBeCalled();

            // act
            var result = sut.Create(new EditProjectViewModel()) as ViewResult;

            // assert
            Mock.Assert(dataMock.Projects);
        }

        [TestMethod]
        public void EditPageReturnsCorrectView()
        {
            // arrange
            string projectId = "projectId";

            Mock.Arrange(() => dataMock.Projects.GetByID(projectId)).Returns(new Project());

            // act
            var result = sut.Edit(projectId) as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(EditProjectViewModel));
        }

        [TestMethod]
        public void EditPostInvokesCorrectMethod()
        {
            // arrange
            Mock.Arrange(() => dataMock.Projects.Update(Arg.IsAny<Project>())).MustBeCalled();

            // act
            var result = sut.Edit(Arg.AnyString, new EditProjectViewModel()) as ViewResult;

            // assert
            Mock.Assert(dataMock.Projects);
        }
    }
}
