using DynamicTimeTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicTimeTable.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TimeTableInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            TempData["WorkingDays"] = model.WorkingDays;
            TempData["SubjectsPerDay"] = model.SubjectsPerDay;
            TempData["TotalSubjects"] = model.TotalSubjects;
            TempData["TotalHours"] = model.TotalHours;

            return RedirectToAction("AllocateSubjects", "TimeTable");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
