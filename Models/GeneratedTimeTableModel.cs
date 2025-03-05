using System.Collections.Generic;

namespace DynamicTimeTable.Models
{
    public class GeneratedTimeTableModel
    {
        public int WorkingDays { get; set; }
        public int SubjectsPerDay { get; set; }
        public List<List<string>> Timetable { get; set; }
    }
}
