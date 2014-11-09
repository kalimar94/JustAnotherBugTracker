using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Models.Repositories.Units
{
    public class ProductProjectUserUnit : IDisposable
    {
        private ApplicationDbContext context;

        public Repository<Project> Projects { get; private set; }

        public Repository<Product> Products { get; private set; }

        public Repository<User> Users { get; private set; }

        public ProductProjectUserUnit(ApplicationDbContext context)
        {
            this.context = context;
            this.Projects = new Repository<Project>(context);
            this.Products = new Repository<Product>(context);
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