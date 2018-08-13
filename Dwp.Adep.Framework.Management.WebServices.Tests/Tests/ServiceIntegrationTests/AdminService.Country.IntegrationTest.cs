using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwp.Adep.Framework.Management.WebServices;
using Dwp.Adep.Framework.Management.WebServices.DataContracts;
using Dwp.Adep.Framework.Management.WebServices.ServiceContracts;

namespace Dwp.Adep.Framework.Management.WebServices.Tests.ServiceIntegrationTests
{
    /// <summary>
    /// Summary description for AdminServiceUnitTest
    /// </summary>
    [TestClass]
    public partial class AdminServiceIntegrationTest
    {
        public AdminServiceIntegrationTest()
        {
            BootStrapper.InitializeIoc();
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #region Get

        [TestMethod()]
        public void GetAllCountry()
        {
            AdminService Service = new AdminService();

            //List<CountryDC> Items = Service.GetAllCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1");

            //Assert.IsTrue(Items.Count() > 1);
        }

        [TestMethod()]
        public void GetCountry()
        {
            AdminService Service = new AdminService();

            //CountryDC Item = Service.GetCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1", "0F0D358A-7FF6-4831-93EF-0FA75369452D");

            //Assert.IsNotNull(Item);
        }

        #endregion

        #region CreateAndDeleteCountry

        [TestMethod()]
        public void CreateAndDeleteCountry()
        {
            //AdminService Service = new AdminService();

            //CountryDC Item = new CountryDC();
            //Item.Code = Guid.NewGuid();
            //Item.CountryCode = "999";
            //Item.Description = "New Test Country";
            //Item.IsANZAC = false;
            //Item.IsEC = true;
            //Item.IsRA = false;
            //Item.Nationality = "New Testish";
            //Item.Notes = "This is a test country";

            //Service.CreateCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1", Item);

            //CountryDC Item2 = Service.GetCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1", Item.Code.ToString());

            //Assert.IsNotNull(Item2);

            //Service.DeleteCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1", Item2.Code.ToString(), Item2.Code.ToString());

            //CountryDC Item3 = Service.GetCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1", Item2.Code.ToString());

            //Assert.IsNull(Item3);
        }

        #endregion

        #region UpdateAndRestoreCountry

        [TestMethod()]
        public void UpdateAndRestoreCountry()
        {
            //AdminService Service = new AdminService();

            //CountryDC Item = Service.GetCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1", "0F0D358A-7FF6-4831-93EF-0FA75369452D");
            
            //string oldDescription = Item.Description;
            
            //Item.Description += "*UPDATED*";

            //Service.UpdateCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1", Item);

            //CountryDC Item2 = Service.GetCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1", "0F0D358A-7FF6-4831-93EF-0FA75369452D");

            //Assert.AreEqual(Item.Description, Item2.Description);

            //Item2.Description = oldDescription;

            //Service.UpdateCountry("jandrews", "jandrews", Guid.NewGuid().ToString(), "1", Item2);

            //Assert.AreEqual(Item.Description, Item2.Description + "*UPDATED*");
        }

        #endregion

    }
}
