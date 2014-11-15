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
        [RegularExpression(@"^[\w]+$", ErrorMessage = "No symbols are allowed on this field except '_'")]
        [MaxLength(10)]
        public string Id { get; set; }

        [Required]
        [RegularExpression(@"^[\w|\s]+$", ErrorMessage = "Special characters are not allowed in this field")]
        [MaxLength(30)]
        public string Name { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}