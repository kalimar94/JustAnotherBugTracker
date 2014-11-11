using BugTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Data.Repositories.Units
{
    public class IssueProjectUserUnit : IDisposable
    {
        private ApplicationDbContext context;

        public Repository<Project> Projects { get; private set; }

        public Repository<Issue> Issues { get; private set; }

        public Repository<User> Users { get; private set; }

        public IssueProjectUserUnit(ApplicationDbContext context)
        {
            this.context = context;
            this.Projects = new Repository<Project>(context);
            this.Issues = new Repository<Issue>(context);
            this.Users = new Repository<User>(context);
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }

        #region IDisposable implementation
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}