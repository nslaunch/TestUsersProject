using AutoMapper;
using UsersApplication.Core;
using UserApplication.Dtos;
using UserApplication.Entities.TestDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApplication.Services
{
    public interface IUserService
    {
        List<UserDto.User> GetUsers();
        UserDto.User GetUser(int id);

        UserDto.User AddUser(UserDto.User user);

        UserDto.User DeleteUser(int id);
    }

    public class UserService : IUserService
    {

        private TestDbContext _testDbContext;

        private IMapper _mapper;

        public UserService(TestDbContext testDbContext, IMapper mapper)
        {
            _testDbContext = testDbContext;
            _mapper = mapper;
        }

        public UserDto.User AddUser(UserDto.User user)
        {
            Users users = _mapper.Map<Users>(user);

            //full name
            if (string.IsNullOrWhiteSpace(user.FullName))
            {
                throw new Exception("Full name is required!");
            }

            //email address
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new Exception("Email is required!");
            }
            else
            {
                if (!user.Email.IsValidEmail())
                {
                    throw new Exception("Invalid email address!");
                }
            }

            _testDbContext.Users.Add(users);
            _testDbContext.SaveChanges();

            return _mapper.Map<UserDto.User>(users);
        }

        public UserDto.User DeleteUser(int id)
        {
            Users users = _testDbContext.Users.Where(w => w.Id == id).FirstOrDefault();

            if (users == null)
                return null;

            _testDbContext.Users.Remove(users);
            _testDbContext.SaveChanges();

            return _mapper.Map<UserDto.User>(users);
        }

        public UserDto.User GetUser(int id)
        {
            Users users = _testDbContext.Users.Where(w => w.Id == id).FirstOrDefault();

            return _mapper.Map<UserDto.User>(users);
        }

        public List<UserDto.User> GetUsers()
        {
            List<Users> users = _testDbContext.Users.ToList();

            return _mapper.Map<List<UserDto.User>>(users);
        }
    }
}
