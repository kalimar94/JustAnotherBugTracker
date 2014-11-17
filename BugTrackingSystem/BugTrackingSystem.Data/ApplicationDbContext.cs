using BugTrackingSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BugTrackingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public IDbSet<Product> Products { get; set; }

        public IDbSet<Project> Projects { get; set; }

        public IDbSet<Issue> Issues { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Ticket> Tickets { get; set; }

        public IDbSet<IssueComment> IssueComments { get; set; }

        public IDbSet<TicketComment> TicketComments { get; set; }

        public IDbSet<Worklog> Worklogs { get; set; }

    }
}