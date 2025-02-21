
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Models;

namespace FirstWebMVC.Controllers;

    public class StudentController : Controller
    {
        private static List<Student> students = new List<Student>();

        public ActionResult List()
        {
            return View(students);
        }

        public ActionResult Add()
        {
            return View();
        }
        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        [HttpPost]
        public ActionResult Add(Student student)
        {
            if (ModelState.IsValid)
            {
                students.Add(student);
                return RedirectToAction("List");
            }

            return View(student);
        }
    }
