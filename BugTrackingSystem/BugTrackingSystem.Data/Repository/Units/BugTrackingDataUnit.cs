using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTrackingSystem.Data.Repository.Units
{
    public class BugTrackingDataUnit : IBugTrackingData
    {
        private ApplicationDbContext context;

        public BugTrackingDataUnit(ApplicationDbContext context)
        {
            this.context = context;
            this.Products = new Repository<Product>(context);
            this.Projects = new Repository<Project>(context);
            this.Issues = new Repository<Issue>(context);
            this.Comments = new Repository<IssueComment>(context);
            this.WorkLogs = new Repository<Worklog>(context);
            this.Roles = new Repository<IdentityRole>(context);
            this.Users = new Repository<User>(context);
        }

        public IRepository<Project> Projects { get; private set; }
         
        public IRepository<Product> Products { get; private set; }
         
        public IRepository<Issue> Issues { get; private set; }
         
        public IRepository<IssueComment> Comments { get; private set; }
         
        public IRepository<Worklog> WorkLogs { get; private set; }

        public IRepository<User> Users { get; private set; }

        public IRepository<IdentityRole> Roles { get; private set; }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
