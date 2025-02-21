using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
/*
 * nuget package: xunit.runner.console
 console host
 setting parallelism options
 C:\Users\NitinJaysingSawant\.nuget\packages\xunit.runner.console\2.9.3\tools\net6.0

dotnet test --logger "console;verbosity=normal"

https://github.com/xunit/visualstudio.xunit/issues/395
https://github.com/xunit/visualstudio.xunit/issues/191
 */
namespace UsersApplication.Tests.UnitTests.Paralellism
{
    #region Runs sequentially in same collection
    [Collection("Run Collection #1")]
    public class SequentialSameCollectionTestClass1
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public SequentialSameCollectionTestClass1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Test2()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }
    }

    [Collection("Run Collection #1")]
    public class SequentialSameCollectionTestClass2
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public SequentialSameCollectionTestClass2(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Test2()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }
    }
    #endregion

    #region Runs sequentially in same class
    //Tests within the same test class will not run in parallel against each other
    public class SequentialSameClassTestClass3
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public SequentialSameClassTestClass3(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Test2()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }
    }
    #endregion

    #region Runs Parallel in different classes or collection
    //able to run in parallel against one another
    [Collection("Run Collection #3")]
    public class ParallelInDifferentClassTestClass4
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public ParallelInDifferentClassTestClass4(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Test2()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }
    }

    [Collection("Run Collection #4")]
    public class ParallelInDifferentClassTestClass5
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public ParallelInDifferentClassTestClass5(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Test2()
        {
            Thread.Sleep(3000);
            Assert.Equal(1, 1);
        }
    }
    #endregion
}
