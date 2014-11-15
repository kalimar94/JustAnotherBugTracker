using BugTrackingSystem.Models;
using BugTrackingSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BugTrackingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Issue> Issues { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Ticket> Tickets { get; set; }

        public System.Data.Entity.DbSet<BugTrackingSystem.Models.IssueComment> IssueComments { get; set; }

        public System.Data.Entity.DbSet<BugTrackingSystem.Models.TicketComment> TicketComments { get; set; }

        public System.Data.Entity.DbSet<BugTrackingSystem.Models.Worklog> Worklogs { get; set; }
    }
}