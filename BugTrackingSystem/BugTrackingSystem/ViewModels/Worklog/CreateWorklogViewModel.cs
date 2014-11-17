using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.ViewModels.Worklog
{
    public class CreateWorklogViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(0.001, 25)]
        public int HoursWorked { get; set; }

    }
}