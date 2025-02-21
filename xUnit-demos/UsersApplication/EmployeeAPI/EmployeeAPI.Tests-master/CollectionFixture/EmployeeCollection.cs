
namespace EmployeeAPI.Tests
{
    [CollectionDefinition("Employee collection")]
    public class EmployeeCollection: ICollectionFixture<EmployeeFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.

        // xUnit.net treats collection fixtures in much the same way as class fixtures,
        // except that the lifetime of a collection fixture object is longer: it is created
        // before any tests are run in any of the test classes in the collection, and will not
        // be cleaned up until all test classes in the collection have finished running.
    }
}
