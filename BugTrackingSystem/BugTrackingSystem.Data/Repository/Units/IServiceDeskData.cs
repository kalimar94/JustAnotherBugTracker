using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Data.Repository.Units
{
    public interface IServiceDeskData
    {
        IRepository<Ticket> Tickets { get;}

        IRepository<Product> Products { get;}

        IRepository<TicketComment> Comments { get;}

        void SaveChanges();
    }
}
