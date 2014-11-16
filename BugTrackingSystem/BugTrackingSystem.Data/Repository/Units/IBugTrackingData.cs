using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.Data.Repository.Units
{
    public interface IBugTrackingData
    {
        IRepository<Project> Projects { get; }

        IRepository<Product> Products { get; }

        IRepository<Issue> Issues { get; }

        IRepository<IssueComment> Comments { get; }

        IRepository<Worklog> WorkLogs { get; }

        IRepository<User> Users { get; }

        IRepository<IdentityRole> Roles { get; }

        void SaveChanges();

    }
}
