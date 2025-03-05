using DynamicTimeTable.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicTimeTable.Controllers
{
    public class TimeTableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllocateSubjects()
        {
            int totalHours = (int)TempData["TotalHours"];
            int totalSubjects = (int)TempData["TotalSubjects"];

            SubjectAllocationModel model = new SubjectAllocationModel
            {
                TotalHours = totalHours,
                Subjects = new List<SubjectHours>()
            };

            for (int i = 0; i < totalSubjects; i++)
                model.Subjects.Add(new SubjectHours());

            return View(model);
        }

        [HttpPost]
        public IActionResult AllocateSubjects(SubjectAllocationModel model)
        {
            if (model.Subjects.Sum(s => s.Hours) != model.TotalHours)
            {
                ModelState.AddModelError("TotalHours", "Total hours allocated must match total hours for the week.");
                return View(model);
            }

            TempData["Subjects"] = JsonConvert.SerializeObject(model.Subjects);

            return RedirectToAction("GenerateTimeTable");
        }

        public IActionResult GenerateTimeTable()
        {
            int workingDays = Convert.ToInt32(TempData["WorkingDays"]);
            int subjectsPerDay = Convert.ToInt32(TempData["SubjectsPerDay"]);

            List<SubjectHours> subjects = JsonConvert.DeserializeObject<List<SubjectHours>>(TempData["Subjects"] as string);

            List<string> subjectPool = new List<string>();

            foreach (var subject in subjects)
                subjectPool.AddRange(Enumerable.Repeat(subject.SubjectName, subject.Hours));

            List<List<string>> timetable = new List<List<string>>();

            for (int i = 0; i < subjectsPerDay; i++)
            {
                List<string> row = new List<string>();

                for (int j = 0; j < workingDays; j++)
                {
                    if (subjectPool.Count > 0)
                    {
                        string selectedSubject = subjectPool[0];
                        row.Add(selectedSubject);
                        subjectPool.RemoveAt(0);
                    }
                }
                timetable.Add(row);
            }

            GeneratedTimeTableModel model = new GeneratedTimeTableModel
            {
                WorkingDays = workingDays,
                SubjectsPerDay = subjectsPerDay,
                Timetable = timetable
            };

            return View(model);
        }

    }
}
