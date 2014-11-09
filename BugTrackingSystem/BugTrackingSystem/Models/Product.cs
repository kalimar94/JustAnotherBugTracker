using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}