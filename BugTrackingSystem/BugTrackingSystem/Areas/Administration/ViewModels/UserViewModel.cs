using BugTrackingSystem.App_Start.AutomapperConfig;
using BugTrackingSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Areas.Administration.ViewModels
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public IEnumerable<IdentityUserRole> Roles { get; set; }
    }
}