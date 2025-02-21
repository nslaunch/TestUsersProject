    using Microsoft.AspNetCore.Mvc;
    using EmployeeAPI.Data;
    using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class EmployeesController : ControllerBase
        {
            private readonly EmployeeContext _context;

            public EmployeesController(EmployeeContext context)
            {
                _context = context;
            }

            [HttpGet]
            public ActionResult<IEnumerable<Employee>> GetEmployees()
            {
                return _context.Employee.ToList();
            }

            [HttpGet("{id}")]
            public ActionResult<Employee> GetEmployee(int id)
            {
                var employee = _context.Employee.Find(id);

                if (employee == null)
                {
                    return NotFound();
                }

                return employee;
            }

            [HttpPost]
            public ActionResult<Employee> PostEmployee(Employee employee)
            {
                _context.Employee.Add(employee);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
            }

            [HttpPut("{id}")]
            public IActionResult PutEmployee(int id, Employee employee)
            {
                if (id != employee.Id)
                {
                    return BadRequest();
                }

            //_context.Entry(employee).State = EntityState.Modified;
            //_context.SaveChanges();

            //return NoContent();

            var existingEmployee = _context.Employee.Find(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Position = employee.Position;
            existingEmployee.Salary = employee.Salary;

            _context.SaveChanges();

            return NoContent();
        }

            [HttpDelete("{id}")]
            public IActionResult DeleteEmployee(int id)
            {
                var employee = _context.Employee.Find(id);

                if (employee == null)
                {
                    return NotFound();
                }

                _context.Employee.Remove(employee);
                _context.SaveChanges();

                return NoContent();
            }

    }
}
    