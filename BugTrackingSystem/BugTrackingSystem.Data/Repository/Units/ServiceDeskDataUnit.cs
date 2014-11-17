using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Data.Repository.Units
{
    public class ServiceDeskDataUnit : IServiceDeskData
    {
        private ApplicationDbContext context;

        public ServiceDeskDataUnit(ApplicationDbContext context)
        {
            this.context = context;
            this.Products = new Repository<Product>(context);
            this.Tickets = new Repository<Ticket>(context);
            this.Comments = new Repository<TicketComment>(context);
        }

        public IRepository<Ticket> Tickets { get; private set; }

        public IRepository<Product> Products { get; private set; }

        public IRepository<TicketComment> Comments { get; private set; }


        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
