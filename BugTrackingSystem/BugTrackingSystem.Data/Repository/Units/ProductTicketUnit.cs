using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;
using System;

namespace BugTrackingSystem.Data.Repository.Units
{
    public class ProductTicketUnit
    {
        private ApplicationDbContext context;

        public Repository<Ticket> Tickets { get; set; }

        public Repository<Product> Products { get; set; }

        public ProductTicketUnit(ApplicationDbContext context)
        {
            this.context = context;
            this.Products = new Repository<Product>(context);
            this.Tickets = new Repository<Ticket>(context);
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
