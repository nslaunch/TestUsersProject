using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementApp.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string  Name { get; set; }

        public Department()
        { 
        
        }

        public Department(int deptId, string code, string name)
        {
            DepartmentId = deptId;
            Code = code;
            Name = name;
        }

    }
}