using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HostelManagementSystem.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        [EmailAddress]
        public string Email { get; set; }   


        public string Address { get; set; } 


        public bool IsDeleted { get; set; }



    }
}