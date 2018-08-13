using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwp.Adep.Framework.Management.WebServices;
using Dwp.Adep.Framework.Management.WebServices.DataContracts;
//using Dwp.Adep.Framework.Management.DataServices.Tests.TestEntityBuilder;

namespace Dwp.Adep.Framework.Management.WebServices.Tests.ServiceUnitTests
{
    /// <summary>
    /// Summary description for AdminServiceUnitTest
    /// </summary>
    [TestClass]
    public partial class AdminServiceUnitTest
    {
        public AdminServiceUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
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

        /// <summary>
        ///A test for GetAllCountryShouldReturnAllCountryItems
        ///</summary>

        [TestMethod()]
        public void GetAllCountryShouldReturnAllCountryItems()
        {
            //CountryDC Country = CountryBuilder.Create();
            //CountryDC Country2 = CountryBuilder.Create();

            //AdminService Service = new AdminService();

            //Service.

            //AgreementClaims = new FakeObjectSet<AgreementClaim>();
            //AgreementClaims.AddObject(AgreementClaimBuilder.Create().WithAgreementCode(Agreement1.Code).WithAgreement(Agreement1).WithIsVersionLatest(true).WithAgreementClaimStatus(AgreementClaimStatus.Status.Authorised));
            //AgreementClaims.AddObject(AgreementClaimBuilder.Create().WithAgreementCode(Agreement1.Code).WithAgreement(Agreement1).WithIsVersionLatest(true).WithAgreementClaimStatus(AgreementClaimStatus.Status.Authorised));
            //AgreementClaims.AddObject(AgreementClaimBuilder.Create().WithAgreementCode(Agreement2.Code).WithAgreement(Agreement2).WithIsVersionLatest(true).WithAgreementClaimStatus(AgreementClaimStatus.Status.Authorised));
            //AgreementClaims.AddObject(AgreementClaimBuilder.Create().WithAgreementCode(Agreement2.Code).WithAgreement(Agreement2).WithIsVersionLatest(true).WithAgreementClaimStatus(AgreementClaimStatus.Status.Authorised));
            //AgreementClaims.AddObject(AgreementClaimBuilder.Create().WithAgreementCode(Agreement2.Code).WithAgreement(Agreement2).WithIsVersionLatest(true).WithAgreementClaimStatus(AgreementClaimStatus.Status.Authorised));

            //FakeESF2007Entities ThisContext = _context as FakeESF2007Entities;
            //ThisContext.AgreementClaims = AgreementClaims;

            //AgreementClaimRepository target = new AgreementClaimRepository(_context);

            //var output = target.GetAllAgreementClaimsByAgreementCode(Agreement1.Code.ToString());

            //Assert.IsTrue(AgreementClaims.Where(x => x.Agreement.Code == Agreement1.Code).Count() == output.Count());
        }
    }
}
