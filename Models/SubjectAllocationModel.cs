using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DynamicTimeTable.Models
{
    public class SubjectAllocationModel
    {
      public int TotalHours { get; set; }
      public List<SubjectHours> Subjects { get; set; }
    }
    public class SubjectHours
    {
        [Required]
        public string SubjectName { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Hours must be a positive number")]
        public int Hours { get; set; }
    }
}
