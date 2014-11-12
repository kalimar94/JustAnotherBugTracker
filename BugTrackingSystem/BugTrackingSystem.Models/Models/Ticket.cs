using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product RelatedProduct { get; set; }

    }
}
