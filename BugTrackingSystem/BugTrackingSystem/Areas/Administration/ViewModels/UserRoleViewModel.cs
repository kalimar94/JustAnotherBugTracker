using BugTrackingSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTrackingSystem.Areas.Administration.ViewModels
{
    public class UserRoleViewModel
    {
        public IdentityRole Role { get; set; }

        public IEnumerable<User>  UsersInRole { get; set; }

        public IEnumerable<SelectListItem> AvailableUsers { get; set; }

        public User NewUser { get; set; }
    }
}