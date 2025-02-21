using UserApplication.Dtos;
using System;
using Xunit;

namespace UserApplication.Theory
{
    public class UserTheoryDataValid: TheoryData<UserDto.User>
    {
        public UserTheoryDataValid()
        {
            /**
             * Each item you add to the TheoryData collection will try to pass your unit test's one by one.
             */

            Add(new UserDto.User()
            {
                Email = "testuser@test.com",
                CreateDate = DateTime.Now,
                FullName = "test user",
                Id = 3,
                Status = true,
            });
        }
    }

    public class UserTheoryDataInvalidEmail : TheoryData<UserDto.User>
    {
        public UserTheoryDataInvalidEmail()
        {
            /**
             * Each item you add to the TheoryData collection will try to pass your unit test's one by one.
             */

            Add(new UserDto.User()
            {
                Email = "testuser@test.com.",
                CreateDate = DateTime.Now,
                FullName = "test user",
                Id = 4,
                Status = true,
            });
        }
    }

    public class UserTheoryDataInvalidName : TheoryData<UserDto.User>
    {
        public UserTheoryDataInvalidName()
        {
            /**
             * Each item you add to the TheoryData collection will try to pass your unit test's one by one.
             */

            Add(new UserDto.User()
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
