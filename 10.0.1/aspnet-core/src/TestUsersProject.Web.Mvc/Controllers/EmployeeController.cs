using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using TestUsersProject.Users.Dto;
using TestUsersProject.Controllers;
using Abp.Web.Models;

namespace TestUsersProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : TestUsersProjectControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService userService)
        {
            _employeeService = userService;
        }

        public string Test1 { get; set; }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new[] { "TestUsersProject web api" });
        }


        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var employee = _employeeService.GetEmployee(id);

            if (employee == null)
                return BadRequest("Employee not found!");

            return Ok(employee);
        }

        [HttpGet("all")]
        public IActionResult GetEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            if (employees == null || employees.Count == 0)
                return BadRequest("Employee not found!");
            return Ok(employees);
        }


        [HttpPost("add")]
        public IActionResult Add([FromBody] EmployeeDto employeeInfo)
        {
            try
            {
                employeeInfo.CreateDate = DateTime.Now;
                var user = _employeeService.AddEmployee(employeeInfo);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to add employee! " + ex.Message);
            }
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] EmployeeDto employeeInfo)
        {
            try
            {
                var user = _employeeService.UpdateEmployee(employeeInfo);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update employee! " + ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _employeeService.DeleteEmployee(id);

            if (user == null)
                return BadRequest("Failed to delete employee!");

            return Ok(user);
        }

        [HttpGet("longrunning")]
        [DontWrapResult(WrapOnError = false, WrapOnSuccess = false, LogError = true)]
        public async Task<IActionResult> LongRunning()
        {
            try
            {
                HttpClient client = new HttpClient();
                string httpResponse = await client.GetStringAsync("https://ip-api.io/api/v1/ip/");
                //dynamic strData = Newtonsoft.Json.JsonConvert.DeserializeObject(httpResponse);
                return Content(httpResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Unexpected error! " + ex.Message);
            }
        }
    }
}
