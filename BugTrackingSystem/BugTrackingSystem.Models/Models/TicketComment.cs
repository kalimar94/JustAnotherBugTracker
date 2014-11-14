using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models
{
    public class TicketComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public string AuthorEmail { get; set; }

        [DataType(DataType.MultilineText)]
        public string commentText { get; set; }

        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }

    }
}
