using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class TeacherUsersModel
    {
        public TeacherUsersModel() {
            Students = new List<Customer>();
        }
        public Customer Teacher { get; set; }

        public List<Customer> Students { get; set; }
    }
}