using EmployeeManagementApp.BLL;
using EmployeeManagementApp.Models;
using EmployeeManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementApp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        EmployeeManager em = new EmployeeManager();
        EmployeeDepartmentManager edm = new EmployeeDepartmentManager();
        public ActionResult Index()
        {
            // Fetch employees with their department details
            List<EmployeeDepartmentViewModel> employeeDepartmentList = edm.GetAllEmployeeDepartment();

            // Fetch departments for dropdowns
            DepartmentManager dm = new DepartmentManager();
            var departments = dm.GetDepartments();

            // Pass departments to the view via ViewBag
            ViewBag.Departments = departments;

            // Return the employee list to the view
            return View(employeeDepartmentList);
        }


        public ActionResult KendoUI()
        {
            // Fetch employees with their department details
            List<EmployeeDepartmentViewModel> employeeDepartmentList = edm.GetAllEmployeeDepartment();

            // Fetch departments for dropdowns
            DepartmentManager dm = new DepartmentManager();
            var departments = dm.GetDepartments();

            // Pass departments to the view via ViewBag
            ViewBag.Departments = departments;

            // Return the employee list to the view
            return View(employeeDepartmentList);
        }

        public ActionResult Create()
        {
            DepartmentManager dm = new DepartmentManager();
            var departments = dm.GetDepartments();
            return View(departments);
        }


        [HttpPost]
        public ActionResult Create(Employee employee)
        {

            Employee emp = new Employee(employee.EmployeeName, employee.Designation, employee.NID, employee.JoiningDate, employee.DepartmentId, employee.BloodGroup);
            string isSaved = em.SaveEmployee(emp);
            ViewBag.IsSaved = isSaved;

            if (isSaved.Length > 0)
            {
                // Redirect to the "About" view upon success.
                return RedirectToAction("Index");
            }
            else
            {
                // Optionally set a ViewBag property to show an error message.
                ViewBag.IsSaved = "Error";
                DepartmentManager dm = new DepartmentManager();
                var departments = dm.GetDepartments();
                return View(departments);
            }
            //return RedirectToAction("Index");
            //return View(departments);
        }
        public ActionResult Delete(int id)
        {
            Console.WriteLine("Calling stored procedure: DeleteEmployee with EmployeeId = xxxxxxx"); // Log stored procedure call
            System.Diagnostics.Debug.WriteLine("Calling stored procedure: DeleteEmployee with EmployeeId = xxxxxxx");
            Console.WriteLine("Calling stored procedure: DeleteEmployee with EmployeeId = " + id); // Log stored procedure call
            System.Diagnostics.Debug.WriteLine("Calling stored procedure: DeleteEmployee with EmployeeId = xxxxxxx" + id);

            //Employee emp = new Employee(employee.Name, employee.Designation, employee.NID, employee.JoiningDate, employee.DepartmentId, employee.BloodGroup);
            string isSaved = em.DeleteEmployee(id);

            return RedirectToAction("Index");
            //DepartmentManager dm = new DepartmentManager();
            //var departments = dm.GetDepartments();
            //return View(departments);
        }
        [HttpDelete]
        public ActionResult Deletez(int id)
        {
            Console.WriteLine("Calling stored procedure: DeleteEmployee with EmployeeId = " + id); // Log stored procedure call
            System.Diagnostics.Debug.WriteLine("Calling stored procedure: DeleteEmployee with EmployeeId = xxxxxxx" + id);

            //Employee emp = new Employee(employee.Name, employee.Designation, employee.NID, employee.JoiningDate, employee.DepartmentId, employee.BloodGroup);
            string isSaved = em.DeleteEmployee(id);

            return RedirectToAction("Index");
            //return View(departments);
        }

        [HttpGet]
        public JsonResult GetDepartments()
        {
            DepartmentManager dm = new DepartmentManager();
            var departments = dm.GetDepartments().Select(d => new
            {
                text = d.Name,
                value = d.DepartmentId
            }).ToList();

            return Json(departments, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(Employee employee)
        {
            try
            {
                // Ensure EmployeeId is passed and used in the update process
                string result = em.UpdateEmployee(employee); // Assuming this method requires EmployeeId

                if (result == "Success")
                {
                    return Json("Success");
                }
                return Json("Failed");
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message);
            }
        }



    }
}

