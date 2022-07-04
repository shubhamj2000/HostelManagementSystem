using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HostelManagementSystem.Models;

namespace HostelManagementSystem.DbOperations
{
    public class StudentRepository
    {

        public int AddStudent(StudentModel model)
        {

            using (var context = new StudentDBEntities1())
            {

                Student std = new Student()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Address = model.Address,
                    DOB=model.DOB,
                    GenderId=model.GenderId,
                    isActive = model.isActive,
                };




                context.Students.Add(std);

                context.SaveChanges();

                return std.Id;


            }



        }



        public List<Models.GenderModel> GetGenders()
        {

            try
            {
                using (var context = new StudentDBEntities1())
                {

                    var abc = context.Genders.Select(x => new Models.GenderModel
                    {
                        id = x.id,
                        Name = x.Name
                    }).ToList();

                    return abc;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }







        public List<StudentModel> GetAllStudents()
        {

            using (var context = new StudentDBEntities1())
            {

                List<StudentModel> result = context.Students.Where(x=>x.isDeleted == false )
                    .Select(x => new StudentModel()
                    {
                        Id = x.Id,


                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Address = x.Address,
                        DOB = (DateTime)x.DOB,
                        gender= new Models.GenderModel() { 
                        
                        Name=x.Gender.Name,

                        },
                         isActive = x.isActive,


                    }).ToList();

                return result;


            }





        }

            

        public bool MultipleDelete(int[] id)
        {
            bool check = false;
            if (id!=null) {
                using (var context = new StudentDBEntities1())
                {
                    var del = context.Students.Where(x => id.Contains(x.Id) && x.isDeleted == false).ToList();

                    foreach (var item in del)
                    {
                        item.isDeleted = true;
                    }
                    context.SaveChanges();
                    check = true;
                    return check;
                }
            }
            return check;
        }



        public StudentModel GetStudent(int id)
        {

            using (var context = new StudentDBEntities1())
            {

                var result = context.Students
                    .Where(x => x.Id == id).
                    Select(x => new StudentModel()
                    {


                        Id = x.Id,

                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Address = x.Address,
                        DOB =  (DateTime)x.DOB,
                        //Models.GenderId= x.GenderId
                        GenderId = x.GenderId,
                        isActive = x.isActive

                    }).FirstOrDefault();

                return result;


            }


        }





        public bool UpdateStudent(int id, StudentModel model)
        {
            using (var context = new StudentDBEntities1())
            {

                var Student = new Student();

                Student.Id = model.Id;
                Student.FirstName = model.FirstName;
                Student.LastName = model.LastName;
                Student.Email = model.Email;
                Student.Address = model.Address;
                Student.DOB= (DateTime)model.DOB;
                Student.GenderId = model.GenderId;
                Student.isActive = model.isActive;

                context.Entry(Student).State = System.Data.Entity.EntityState.Modified;// Entity framework feature
                context.SaveChanges();// Db will hit here only one time
                return true;
            }

        }


        public bool DeleteStudent(int id)
        {
            using (var context = new StudentDBEntities1())
            {

                //var std = new Student()
                //{
                //   Id = id
                //};


                //context.Entry(std).State = System.Data.Entity.EntityState.Deleted; //->Entity framework feature
                var stdc = context.Students.Where(x => x.isDeleted == false && x.Id == id).FirstOrDefault();
                stdc.isDeleted = true;
                context.SaveChanges(); // here Db will hit only one time
                return true;
            }
        }


    }
}