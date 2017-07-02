
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string  FirstName { get; set; }

        public string LastName { get; set; }

        public string Designation { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string EmployeeFullName { get{
        return (FirstName + " "  + LastName);
        }}
        // child keys
        public ICollection<Job> JobCards { get; set; }
    }
}
