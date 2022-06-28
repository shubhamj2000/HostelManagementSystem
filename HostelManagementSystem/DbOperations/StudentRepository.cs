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

            using (var context = new StudentDBEntities())
            {

                Student std = new Student()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Address = model.Address
                };




                context.Student.Add(std);

                context.SaveChanges();

                return std.Id;


            }



        }


        public List<StudentModel> GetAllStudents()
        {

            using (var context = new StudentDBEntities())
            {

                List<StudentModel> result = context.Student.Where(x=>x.isDeleted == false)
                    .Select(x => new StudentModel()
                    {
                        Id = x.Id,


                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Address = x.Address,
                    }).ToList();

                return result;


            }





        }




        public StudentModel GetStudent(int id)
        {

            using (var context = new StudentDBEntities())
            {

                var result = context.Student
                    .Where(x => x.Id == id).
                    Select(x => new StudentModel()
                    {


                        Id = x.Id,

                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Address = x.Address

                    }).FirstOrDefault();

                return result;


            }


        }





        public bool UpdateStudent(int id, StudentModel model)
        {
            using (var context = new StudentDBEntities())
            {

                var Student = new Student();

                Student.Id = model.Id;
                Student.FirstName = model.FirstName;
                Student.LastName = model.LastName;
                Student.Email = model.Email;
                Student.Address = model.Address;

                context.Entry(Student).State = System.Data.Entity.EntityState.Modified;// Entity framework feature
                context.SaveChanges();// Db will hit here only one time
                return true;
            }

        }


        public bool DeleteStudent(int id)
        {
            using (var context = new StudentDBEntities())
            {

                //var std = new Student()
                //{
                //   Id = id
                //};


                //context.Entry(std).State = System.Data.Entity.EntityState.Deleted; //->Entity framework feature
                var stdc = context.Student.Where(x => x.isDeleted == false && x.Id == id).FirstOrDefault();
                stdc.isDeleted = true;
                context.SaveChanges(); // here Db will hit only one time
                return true;
            }
        }


    }
}