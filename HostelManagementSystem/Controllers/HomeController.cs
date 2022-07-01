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
            var model = new StudentModel();
            model.Genders = repository.GetGenders();
           

            return PartialView("_Create",model);
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
                    // ViewBag.Issuccess = " Data Added ";

                    TempData["success"] = "data added";
                }

               

                return RedirectToAction("GetAllRecords");

             

            }



            return RedirectToAction("GetAllRecords");

         //   return View(); ---> previous running state

        }



   

        public ActionResult GetAllRecords()
        {
            //if (TempData["success"] != null) {
            //    ViewBag.issuccess = TempData["success"];
            //}

            List<StudentModel> result = repository.GetAllStudents();
            return View(result);

        }


        public ActionResult Details(int Id)
        {

            var student = repository.GetStudent(Id);
            student.Genders = repository.GetGenders();
            return PartialView("_Details",student);

        }

        public ActionResult Edit(int id)
        {

            var student = repository.GetStudent(id);
            student.Genders = repository.GetGenders();
            //return View(student);

            return PartialView(student);
        }

        [HttpPost]
        public ActionResult Edit(StudentModel model)
        {

            if (ModelState.IsValid)
            {

                repository.UpdateStudent(model.Id, model);
                return RedirectToAction("GetAllRecords");

            }


            return View(model);

        }



        public ActionResult Delete(int id   )
        {

            repository.DeleteStudent(id);

            TempData["dataDeleted"] = "data deleted";

            return RedirectToAction("GetAllRecords");

        }





    }
}