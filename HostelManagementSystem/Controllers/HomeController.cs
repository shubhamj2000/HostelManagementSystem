using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HostelManagementSystem.DbOperations;
using HostelManagementSystem.Models;
using Newtonsoft.Json;


namespace HostelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        StudentRepository repository = null;


        public HomeController()
        {

            repository = new StudentRepository();

        }



        // GET: Home
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentModel model)
        {


            if (ModelState.IsValid)
            {

                int id = repository.AddStudent(model);
                if (id > 0)
                {

                    ModelState.Clear();
                    ViewBag.Issuccess = " Data Added ";

                }


            }





            return View();

        }


        public ActionResult GetAllRecords()
        {

            List<StudentModel> result = repository.GetAllStudents();
            return View(result);

        }


        public ActionResult Details(int Id)
        {

            var student = repository.GetStudent(Id);
            return PartialView("_Details",student);

        }

        public ActionResult Edit(int id)
        {

            var student = repository.GetStudent(id);
            return View(student);

        }

        [HttpPost]
        public ActionResult Edit(StudentModel model)
        {

            if (ModelState.IsValid)
            {

                repository.UpdateStudent(model.Id, model);
                return RedirectToAction("GetAllRecords");

            }


            return View();

        }



        public ActionResult Delete(int id)
        {

            repository.DeleteStudent(id);

            return RedirectToAction("GetAllRecords");

        }





    }
}