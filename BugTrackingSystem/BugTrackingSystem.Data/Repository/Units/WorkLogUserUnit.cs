using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Data.Repository.Units
{
    public class WorkLogUserUnit
    {
        private ApplicationDbContext context;

        public Repository<User> Users { get; set; }

        public Repository<Worklog> WorkLogs { get; set; }

        public WorkLogUserUnit(ApplicationDbContext context)
        {
            this.context = context;
            this.Users = new Repository<User>(context);
            this.WorkLogs = new Repository<Worklog>(context);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
