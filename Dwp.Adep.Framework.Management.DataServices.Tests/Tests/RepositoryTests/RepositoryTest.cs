using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq.Expressions;
using Dwp.Adep.Framework.Management.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManagementConsole.Server.UnitTest
{

    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RepositoryTest
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        ///// <summary>
        /////A test for Find
        /////</summary>
        //public void FindTestHelper<T>()
        //    where T : class
        //{
        //    Moq.Mock<IObjectSet<T>> mockObjectSet = new Moq.Mock<IObjectSet<T>>();

        //    Moq.Mock<IObjectContext> mockObjectContext = new Moq.Mock<IObjectContext>();
        //    mockObjectContext.Setup(x => x.CreateObjectSet<T>()).Returns(mockObjectSet.Object);

        //    Repository<T> target = CreateRepository<T>(); // TODO: Initialize to an appropriate value
        //    Expression<Func<T, bool>> filter = null; // TODO: Initialize to an appropriate value
        //    string[] children = null; // TODO: Initialize to an appropriate value
        //    IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
        //    IEnumerable<T> actual;
        //    actual = target.Find(filter, children);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        //internal virtual Repository<T> CreateRepository<T>()
        //    where T : class
        //{
        //    // TODO: Instantiate an appropriate concrete class.
        //    Repository<T> target = null;
        //    return target;
        //}

        //[TestMethod()]
        //public void FindTest()
        //{

        //    FindTestHelper<GenericParameterHelper>();
        //}

        ///// <summary>
        /////A test for Find
        /////</summary>
        //public void FindTest1Helper<T>()
        //    where T : class
        //{
        //    Repository<T> target = CreateRepository<T>(); // TODO: Initialize to an appropriate value
        //    Expression<Func<T, bool>> filter = null; // TODO: Initialize to an appropriate value
        //    IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
        //    IEnumerable<T> actual;
        //    actual = target.Find(filter);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        //[TestMethod()]
        //public void FindTest1()
        //{
        //    FindTest1Helper<GenericParameterHelper>();
        //}
    }
}
