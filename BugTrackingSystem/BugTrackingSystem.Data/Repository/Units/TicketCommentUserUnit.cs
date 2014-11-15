using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Data.Repository.Units
{
    public class TicketCommentUserUnit
    {
        private ApplicationDbContext context;

        public Repository<TicketComment> Comments { get; set; }

        public Repository<User> Users { get; set; }

        public TicketCommentUserUnit(ApplicationDbContext context)
        {
            this.context = context;
            this.Comments = new Repository<TicketComment>(context);
            this.Users = new Repository<User>(context);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}
