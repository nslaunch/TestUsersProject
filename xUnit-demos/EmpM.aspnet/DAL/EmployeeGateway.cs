using EmployeeManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace EmployeeManagementApp.DAL
{
    public class EmployeeGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["EMS"].ConnectionString;
        public List<Employee> GetAllEmployee()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetEmployees", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            var employeeList = new List<Employee>();
            while (reader.Read())
            {

                int employeeId = Convert.ToInt32(reader["EmployeeId"]);
                string Name = reader["Name"].ToString();
                string Designation = reader["Designation"].ToString();
                string NID = reader["NID"].ToString();
                string BloodGroup = reader["BloodGroup"].ToString();
                DateTime JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);
                int DepartmentId = Convert.ToInt32(reader["Departmentid"]);

                Employee em = new Employee(Name, Designation, NID, JoiningDate, DepartmentId, BloodGroup);
                em.EmployeeId = employeeId;
                employeeList.Add(em);

            }
            reader.Close();
            connection.Close();
            return employeeList;
        }

        public string SaveEmployees(Employee employee)
        {


            SqlConnection connection = new SqlConnection(connectionString);
            


            // Define the stored procedure and connection
            SqlCommand command = new SqlCommand("InsertEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    command.Parameters.AddWithValue("@Designation", employee.Designation);
                    command.Parameters.AddWithValue("@NID", employee.NID);
            //command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
            if (DateTime.TryParse(employee.JoiningDate.ToString(), out DateTime parsedDate))
            {
                command.Parameters.AddWithValue("@JoiningDate", parsedDate);
            }
            else
            {
                throw new Exception("Invalid Joining Date format. Please provide a valid date.");
            }
            command.Parameters.AddWithValue("@BloodGroup", employee.BloodGroup);
                    command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);

                    // Open connection and execute
                    connection.Open();

            
      
            int rowEffect = command.ExecuteNonQuery();
            connection.Close();

            if (rowEffect > 0)
            {
                return "SuccessFull";
            }
            //else
            //{
            //    return StatusCode(500, new { message = "An error occurred while saving the employee." }); // Returns 500 Internal Server Error

            //}
            return "Failed";
        }



        public string DeleteEmployees(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            Console.WriteLine("Opening database connection..."); // Log before opening the connection

            SqlCommand command = new SqlCommand("DeleteEmployee", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add the parameter for the stored procedure
            command.Parameters.AddWithValue("@EmployeeId", id); 
            Console.WriteLine("Calling stored procedure: DeleteEmployee with EmployeeId = " + id); // Log stored procedure call


            // Open connection
            connection.Open();

            // Execute the command
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Rows affected: {rowsAffected}"); // Log the result of the execution

            // Return success message if rows were affected
            return rowsAffected > 0 ? "Success" : "No rows deleted (employee not found).";

        }

        public string UpdateEmployees(Employee employee)
        {
            
                SqlConnection connection = new SqlConnection(connectionString);
                
                    // Define the stored procedure and connection
                    SqlCommand command = new SqlCommand("UpdateEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    command.Parameters.AddWithValue("@Designation", employee.Designation);
                    command.Parameters.AddWithValue("@NID", employee.NID);
                    command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                    command.Parameters.AddWithValue("@BloodGroup", employee.BloodGroup);
                    //command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);

                    // Open connection
                    connection.Open();

                    // Execute the stored procedure
                    int rowsAffected = command.ExecuteNonQuery();

                    // Return success message if rows were affected
                    return rowsAffected > 0 ? "Success" : "No rows updated (employee not found).";
                
        
        }

    }

}

