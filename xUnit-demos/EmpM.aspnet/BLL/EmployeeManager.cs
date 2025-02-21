using EmployeeManagementApp.DAL;
using EmployeeManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementApp.BLL
{
    public class EmployeeManager
    {
        EmployeeGateway gateway = new EmployeeGateway();
        public List<Employee> GetAllEmployee()
        {
           return gateway.GetAllEmployee();
        }

        public string SaveEmployee(Employee employee)
        {

            return gateway.SaveEmployees(employee);
        }
        public string DeleteEmployee(int id)
        {
            return gateway.DeleteEmployees(id);

        }

        public string UpdateEmployee(Employee employee)
        {

            return gateway.UpdateEmployees(employee);
        }
    }
}