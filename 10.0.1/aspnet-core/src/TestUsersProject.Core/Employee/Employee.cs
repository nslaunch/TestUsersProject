using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Entities;

namespace TestUsersProject
{
    [AutoMapTo(typeof(EmployeeDto))]
    public class Employee: Entity<int>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
