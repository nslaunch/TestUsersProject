using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Web.Models;
using AutoMapper;
using TestUsersProject.Authorization.Users;
using TestUsersProject.Users.Dto;

namespace TestUsersProject
{
    public interface IEmployeeService: IApplicationService
    {
        List<EmployeeDto> GetAllEmployees();
        EmployeeDto GetEmployee(int id);

        EmployeeDto AddEmployee(EmployeeDto user);

        EmployeeDto DeleteEmployee(int id);
        EmployeeDto UpdateEmployee(EmployeeDto employeeInfo);
    }

    [DontWrapResult(WrapOnError = false, WrapOnSuccess = false, LogError = true)]
    public class EmployeeService: TestUsersProjectAppServiceBase, IEmployeeService
    {
        private IMapper _mapper;

        public EmployeeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public EmployeeDto AddEmployee(EmployeeDto employeeInfo)
        {
            Employee employee = _mapper.Map<Employee>(employeeInfo);

            //full name
            if (string.IsNullOrWhiteSpace(employee.FullName))
            {
                throw new Exception("Full name is required!");
            }

            //email address
            if (string.IsNullOrWhiteSpace(employee.Email))
            {
                throw new Exception("Email is required!");
            }
            else
            {
                if (!employee.Email.IsValidEmail())
                {
                    throw new Exception("Invalid email address!");
                }
            }

            var entitiy = EmployeeRepo.Insert(employee);

            return _mapper.Map<EmployeeDto>(entitiy);
        }

        public EmployeeDto DeleteEmployee(int id)
        {
            Employee entitiy = EmployeeRepo.GetAll().Where(w => w.Id == id).FirstOrDefault();

            if (entitiy == null)
                return null;

            EmployeeRepo.Delete(entitiy);

            return _mapper.Map<EmployeeDto>(entitiy);
        }

        public EmployeeDto UpdateEmployee(EmployeeDto employeeInfo)
        {
            Employee employee = _mapper.Map<Employee>(employeeInfo);

            //full name
            if (string.IsNullOrWhiteSpace(employee.FullName))
            {
                throw new Exception("Full name is required!");
            }

            //email address
            if (string.IsNullOrWhiteSpace(employee.Email))
            {
                throw new Exception("Email is required!");
            }
            else
            {
                if (!employee.Email.IsValidEmail())
                {
                    throw new Exception("Invalid email address!");
                }
            }

            Employee entity = EmployeeRepo.GetAll().Where(w => w.Id == employee.Id).FirstOrDefault();
            entity.FullName = employee.FullName;
            entity.Email = employee.Email;
            entity.Status = employee.Status;
            EmployeeRepo.Update(entity);

            return _mapper.Map<EmployeeDto>(entity);
        }

        public EmployeeDto GetEmployee(int id)
        {
            Employee entity = EmployeeRepo.GetAll().Where(w => w.Id == id).FirstOrDefault();

            return _mapper.Map<EmployeeDto>(entity);
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            List<Employee> employees = EmployeeRepo.GetAll().ToList();

            return _mapper.Map<List<EmployeeDto>>(employees);
        }
    }
}
