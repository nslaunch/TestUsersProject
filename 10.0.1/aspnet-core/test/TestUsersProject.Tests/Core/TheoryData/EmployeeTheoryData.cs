using System;
using Xunit;

namespace TestUsersProject.Tests.Core.TheoryData
{
    public class EmployeeTheoryDataValid: TheoryData<EmployeeDto>
    {
        public EmployeeTheoryDataValid()
        {
            /**
             * Each item you add to the TheoryData collection will try to pass your unit test's one by one.
             */

            Add(new EmployeeDto()
            {
                Email = "testuser@test.com",
                CreateDate = DateTime.Now,
                FullName = "test user",
                Id = 3,
                Status = true,
            });
        }
    }

    public class EmployeeTheoryDataInvalidEmail : TheoryData<EmployeeDto>
    {
        public EmployeeTheoryDataInvalidEmail()
        {
            /**
             * Each item you add to the TheoryData collection will try to pass your unit test's one by one.
             */

            Add(new EmployeeDto()
            {
                Email = "testuser@test.com.",
                CreateDate = DateTime.Now,
                FullName = "test user",
                Id = 4,
                Status = true,
            });
        }
    }

    public class EmployeeTheoryDataInvalidName : TheoryData<EmployeeDto>
    {
        public EmployeeTheoryDataInvalidName()
        {
            /**
             * Each item you add to the TheoryData collection will try to pass your unit test's one by one.
             */

            Add(new EmployeeDto()
            {
                Email = "testuser@test.com",
                CreateDate = DateTime.Now,
                FullName = null,
                Id = 5,
                Status = true,
            });
        }
    }
}
