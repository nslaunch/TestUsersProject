using AutoMapper;
using UserApplication.Controllers;
using UserApplication.Helpers;
using UserApplication.Services;
using UserApplication.Mock.Entities;
using System;
using UserApplication.Controllers;

namespace UserApplication.Fixture
{
    //share a single object instance among all tests in a test class
    public class ControllerFixture : IDisposable
    {

        private TestDbContextMock testDbContextMock { get; set; }
        private UserService userService { get; set; }
        private IMapper mapper { get; set; }

        public UserController userController { get; private set; }

        public ControllerFixture()
        {
            #region Create mock/memory database

            testDbContextMock = new TestDbContextMock();

            testDbContextMock.Users.AddRange(new UserApplication.Entities.TestDb.Users[]
            {
                // for delete test
                new UserApplication.Entities.TestDb.Users()
                {
                  Id = 1,
                  Email = "test@test.com",
                  FullName = "test name",
                  CreateDate = DateTime.Now,
                  Status = true,
                },
                // for get test
                new UserApplication.Entities.TestDb.Users()
                {
                  Id = 2,
                  Email = "test2@test.com",
                  FullName = "test name2",
                  CreateDate = DateTime.Now,
                  Status = true,
                }
            });

            testDbContextMock.SaveChanges();
            
            #endregion

            #region Mapper settings with original profiles.

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UsersMappingProfile());
            });

            mapper = mappingConfig.CreateMapper();

            #endregion

            // Create UserService with Memory Database
            userService = new UserService(testDbContextMock, mapper);

            // Create Controller
            userController = new UserController(userService);

        }

        #region ImplementIDisposableCorrectly
        /** https://docs.microsoft.com/en-us/visualstudio/code-quality/ca1063?view=vs-2019 */
        /*
            Dispose(true)
            Called from the Dispose() method, which is explicitly called. This releases both managed and unmanaged resources.

            Dispose(false)
            Called from the finalizer, which is implicitly called. This releases only unmanaged resources.
         */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);//It's informing the Garbage Collector (GC) that this object was cleaned up fully.
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources, but leave the other methods
        // exactly as they are.
        ~ControllerFixture()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                testDbContextMock.Dispose();
                testDbContextMock = null;

                userService = null;
                mapper = null;

                userController = null;
            }
        }
        #endregion
    }
}
