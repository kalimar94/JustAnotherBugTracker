using BugTrackingSystem.Data.Repositories;
using BugTrackingSystem.Models;

namespace BugTrackingSystem.Data.Repository.Units
{
    public class CommentUserUnit
    {
        private ApplicationDbContext context;

        public Repository<User> Users { get; set; }

        public Repository<IssueComment> Comments { get; set; }

        public CommentUserUnit(ApplicationDbContext context)
        {
            this.context = context;
            this.Users = new Repository<User>(context);
            this.Comments = new Repository<IssueComment>(context);
        }
    }
}
