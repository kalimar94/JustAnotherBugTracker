using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
