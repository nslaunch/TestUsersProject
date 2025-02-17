using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace TestUsersProject
{
    [AutoMapTo(typeof(Employee))]
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
    }
}
