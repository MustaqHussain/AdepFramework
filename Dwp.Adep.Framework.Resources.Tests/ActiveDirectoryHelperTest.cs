using Dwp.Adep.Framework.Resources.DataContracts;
using Dwp.Adep.Framework.Resources.ServiceContracts.AD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
//using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.Tests
{
    
    
    /// <summary>
    ///This is a test class for ActiveDirectoryHelperTest and is intended
    ///to contain all ActiveDirectoryHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ActiveDirectoryHelperTest
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

        #region GetEmailAddresses tests - valid arguments.
        /// <summary>
        ///A test for GetEmailAddresses
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        [TestMethod]
        public void GetEmailAddresses_ShouldReturnCorrectEmailAddress()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);
            string firstName = "anything";
            string lastName = "anything";
            List<string> expected = new List<string>();
            expected.Add("FindAll@test.com");

            List<string> actual;
            actual = target.GetEmailAddresses(firstName, lastName);
            Assert.AreEqual(expected[0], actual[0]);
        }
        #endregion

        #region GetEmailAddresses tests - invalid argument(s).
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEmailAddresses_ShouldThrowExceptionOnNullFirstName()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);
            string firstName = "";
            string lastName = "anything";

            target.GetEmailAddresses(firstName, lastName);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEmailAddresses_ShouldThrowExceptionOnNullLastName()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);
            string firstName = "anything";
            string lastName = "";

            target.GetEmailAddresses(firstName, lastName);
        }

        [TestMethod]
        public void GetEmailAddresses_ShouldThrowExceptionOnNullFirstNameWithCorrectDebugInformation()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);
            string firstName = "";
            string lastName = "anything";

            try
            {
                target.GetEmailAddresses(firstName, lastName);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "firstName");
                return;
            }

            Assert.Fail("ArgumentNullException was not thrown.");
        }

        [TestMethod]
        public void GetEmailAddresses_ShouldThrowExceptionOnNullLastNameWithCorrectDebugInformation()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);
            string firstName = "anything";
            string lastName = "";

            try
            {
                target.GetEmailAddresses(firstName, lastName);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "lastName");
                return;
            }

            Assert.Fail("ArgumentNullException was not thrown.");
        }
        #endregion

        #region GetEmailAddress tests - valid arguments.
        [TestMethod]
        public void GetEmailAddress_ShouldReturnADUserWithCorrectEmailAddress()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);
            ADUser expectedUser = new ADUser() { Email = "FindEmail@test.com", };

            ADUser actual = target.GetEmailAddress("77777777");

            Assert.AreEqual(expectedUser.Email, actual.Email);
        }
        #endregion

        #region GetEmailAddress tests - invalid argument.
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEmailAddress_ShouldThrowExceptionOnNullAccountName()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            ADUser actual = target.GetEmailAddress("");
        }

        [TestMethod]
        public void GetEmailAddress_ShouldThrowExceptionOnNullAccountNameWithCorrectDebugInformation()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            try
            {
                ADUser actual = target.GetEmailAddress("");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "samAccountName");
                return;
            }

            Assert.Fail("ArgumentNullException was not thrown.");
        }
        #endregion

        #region SearchUserByLogin tests - valid arguments.
        [TestMethod]
        public void SearchUserByLoginTest_ShouldReturnCorrectADUser()
        {
            ADUser expectedUser = new ADUser()
            {
                Email = "FindEmail@test.com",
                DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                DNSHostname = "my Host Name",
                FirstName = "Fred",
                LastName = "Blogs",
                GivenName = "Fred Blogs",
                Login = "bfred",
                ProfilePath = @"\\ServerName\SomeFolderOnServer",
                SN = "sn information",
                TelephoneNumber = "00 00 00 000",
                UserPrincipalName = "bfred",
                OfficeLocation = "ASTON"
            };
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            ADUser actual = target.SearchUserByLogin("77777777");

            Assert.AreEqual(expectedUser.Email, actual.Email);
            Assert.AreEqual(expectedUser.DistinguishedName, actual.DistinguishedName);
            Assert.AreEqual(expectedUser.DNSHostname, actual.DNSHostname);
            Assert.AreEqual(expectedUser.FirstName, actual.FirstName);
            Assert.AreEqual(expectedUser.LastName, actual.LastName);
            Assert.AreEqual(expectedUser.GivenName, actual.GivenName);
            Assert.AreEqual(expectedUser.Login, actual.Login);
            Assert.AreEqual(expectedUser.ProfilePath, actual.ProfilePath);
            Assert.AreEqual(expectedUser.SN, actual.SN);
            Assert.AreEqual(expectedUser.TelephoneNumber, actual.TelephoneNumber);
            Assert.AreEqual(expectedUser.UserPrincipalName, actual.UserPrincipalName);
            Assert.AreEqual(expectedUser.OfficeLocation, actual.OfficeLocation);
        }
        #endregion

        #region SearchUserByLogin tests - invalid argument.
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SearchUserByLogin_ShouldThrowExceptionOnNullAccountName()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            ADUser actual = target.SearchUserByLogin("");
        }

        [TestMethod]
        public void SearchUserByLogin_ShouldThrowExceptionOnNullAccountNameWithCorrectDebugInformation()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            try
            {
                ADUser actual = target.SearchUserByLogin("");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "samAccountName");
                return;
            }

            Assert.Fail();
        }
        #endregion

        #region SearchUserByName tests - valid argument(s).
        [TestMethod]
        public void SearchUserByName_SearchWithFirstNameShouldReturnCorrectListOfADUser()
        {
            var expectedUser = new List<ADUser>()
                {
                    new ADUser()
                    {
                        Email = "FindEmail@test.com",
                        DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                        DNSHostname = "my Host Name",
                        FirstName = "Fred",
                        LastName = "Blogs",
                        GivenName = "Fred Blogs",
                        Login = "bfred",
                        ProfilePath = @"\\ServerName\SomeFolderOnServer",
                        SN = "sn information",
                        TelephoneNumber = "00 00 00 000",
                        UserPrincipalName = "bfred",
                        OfficeLocation = "ASTON"
                    },
                    new ADUser()
                    {
                        Email = "FindEmail1@test.com",
                        DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                        DNSHostname = "my Host Name 2",
                        FirstName = "Fred 2",
                        LastName = "Blogs 2",
                        GivenName = "Fred Blogs 2",
                        Login = "bfred2",
                        ProfilePath = @"\\ServerName\SomeFolderOnServer2",
                        SN = "sn information2",
                        TelephoneNumber = "00 00 00 002",
                        UserPrincipalName = "bfred2",
                        OfficeLocation = "ASTON"
                    }
                };

            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            var actual = target.SearchUserByName("firstname","");

            Assert.AreEqual(expectedUser[0].Login, actual[0].Login);
            Assert.AreEqual(expectedUser[0].FirstName, actual[0].FirstName);
            Assert.AreEqual(expectedUser[0].LastName, actual[0].LastName);
            Assert.AreEqual(expectedUser[0].Email, actual[0].Email);
            Assert.AreEqual(expectedUser[0].DistinguishedName,actual[0].DistinguishedName);
            Assert.AreEqual(expectedUser[0].DNSHostname, actual[0].DNSHostname);
            Assert.AreEqual(expectedUser[0].GivenName, actual[0].GivenName);
            Assert.AreEqual(expectedUser[0].ProfilePath, actual[0].ProfilePath);
            Assert.AreEqual(expectedUser[0].SN, actual[0].SN);
            Assert.AreEqual(expectedUser[0].TelephoneNumber, actual[0].TelephoneNumber);
            Assert.AreEqual(expectedUser[0].UserPrincipalName, actual[0].UserPrincipalName);
            Assert.AreEqual(expectedUser[0].OfficeLocation, actual[0].OfficeLocation);

            Assert.AreEqual(expectedUser[1].Login, actual[1].Login);
            Assert.AreEqual(expectedUser[1].FirstName, actual[1].FirstName);
            Assert.AreEqual(expectedUser[1].LastName, actual[1].LastName);
            Assert.AreEqual(expectedUser[1].Email, actual[1].Email);
            Assert.AreEqual(expectedUser[1].DistinguishedName, actual[1].DistinguishedName);
            Assert.AreEqual(expectedUser[1].DNSHostname, actual[1].DNSHostname);
            Assert.AreEqual(expectedUser[1].GivenName, actual[1].GivenName);
            Assert.AreEqual(expectedUser[1].ProfilePath, actual[1].ProfilePath);
            Assert.AreEqual(expectedUser[1].SN, actual[1].SN);
            Assert.AreEqual(expectedUser[1].TelephoneNumber, actual[1].TelephoneNumber);
            Assert.AreEqual(expectedUser[1].UserPrincipalName, actual[1].UserPrincipalName);
            Assert.AreEqual(expectedUser[1].OfficeLocation, actual[1].OfficeLocation);
        }

        [TestMethod]
        public void SearchUserByName_SearchWithLastNameShouldReturnCorrectListOfADUser()
        {
            var expectedUser = new List<ADUser>()
                {
                    new ADUser()
                    {
                        Email = "FindEmail@test.com",
                        DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                        DNSHostname = "my Host Name",
                        FirstName = "Fred",
                        LastName = "Blogs",
                        GivenName = "Fred Blogs",
                        Login = "bfred",
                        ProfilePath = @"\\ServerName\SomeFolderOnServer",
                        SN = "sn information",
                        TelephoneNumber = "00 00 00 000",
                        UserPrincipalName = "bfred",
                        OfficeLocation = "ASTON"
                    },
                    new ADUser()
                    {
                        Email = "FindEmail1@test.com",
                        DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                        DNSHostname = "my Host Name 2",
                        FirstName = "Fred 2",
                        LastName = "Blogs 2",
                        GivenName = "Fred Blogs 2",
                        Login = "bfred2",
                        ProfilePath = @"\\ServerName\SomeFolderOnServer2",
                        SN = "sn information2",
                        TelephoneNumber = "00 00 00 002",
                        UserPrincipalName = "bfred2",
                        OfficeLocation = "ASTON"
                    }
                };

            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            var actual = target.SearchUserByName("", "lastname");

            Assert.AreEqual(expectedUser[0].Login, actual[0].Login);
            Assert.AreEqual(expectedUser[0].FirstName, actual[0].FirstName);
            Assert.AreEqual(expectedUser[0].LastName, actual[0].LastName);
            Assert.AreEqual(expectedUser[0].Email, actual[0].Email);
            Assert.AreEqual(expectedUser[0].DistinguishedName, actual[0].DistinguishedName);
            Assert.AreEqual(expectedUser[0].DNSHostname, actual[0].DNSHostname);
            Assert.AreEqual(expectedUser[0].GivenName, actual[0].GivenName);
            Assert.AreEqual(expectedUser[0].ProfilePath, actual[0].ProfilePath);
            Assert.AreEqual(expectedUser[0].SN, actual[0].SN);
            Assert.AreEqual(expectedUser[0].TelephoneNumber, actual[0].TelephoneNumber);
            Assert.AreEqual(expectedUser[0].UserPrincipalName, actual[0].UserPrincipalName);
            Assert.AreEqual(expectedUser[0].OfficeLocation, actual[0].OfficeLocation);

            Assert.AreEqual(expectedUser[1].Login, actual[1].Login);
            Assert.AreEqual(expectedUser[1].FirstName, actual[1].FirstName);
            Assert.AreEqual(expectedUser[1].LastName, actual[1].LastName);
            Assert.AreEqual(expectedUser[1].Email, actual[1].Email);
            Assert.AreEqual(expectedUser[1].DistinguishedName, actual[1].DistinguishedName);
            Assert.AreEqual(expectedUser[1].DNSHostname, actual[1].DNSHostname);
            Assert.AreEqual(expectedUser[1].GivenName, actual[1].GivenName);
            Assert.AreEqual(expectedUser[1].ProfilePath, actual[1].ProfilePath);
            Assert.AreEqual(expectedUser[1].SN, actual[1].SN);
            Assert.AreEqual(expectedUser[1].TelephoneNumber, actual[1].TelephoneNumber);
            Assert.AreEqual(expectedUser[1].UserPrincipalName, actual[1].UserPrincipalName);
            Assert.AreEqual(expectedUser[1].OfficeLocation, actual[1].OfficeLocation);
        }

        [TestMethod]
        public void SearchUserByName_SearchWithBothNamesShouldReturnCorrectListOfADUser()
        {
            var expectedUser = new List<ADUser>()
                {
                    new ADUser()
                    {
                        Email = "FindEmail@test.com",
                        DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                        DNSHostname = "my Host Name",
                        FirstName = "Fred",
                        LastName = "Blogs",
                        GivenName = "Fred Blogs",
                        Login = "bfred",
                        ProfilePath = @"\\ServerName\SomeFolderOnServer",
                        SN = "sn information",
                        TelephoneNumber = "00 00 00 000",
                        UserPrincipalName = "bfred",
                        OfficeLocation = "ASTON"
                    },
                    new ADUser()
                    {
                        Email = "FindEmail1@test.com",
                        DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                        DNSHostname = "my Host Name 2",
                        FirstName = "Fred 2",
                        LastName = "Blogs 2",
                        GivenName = "Fred Blogs 2",
                        Login = "bfred2",
                        ProfilePath = @"\\ServerName\SomeFolderOnServer2",
                        SN = "sn information2",
                        TelephoneNumber = "00 00 00 002",
                        UserPrincipalName = "bfred2",
                        OfficeLocation = "ASTON"
                    }
                };

            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            var actual = target.SearchUserByName("firstname", "lastname");

            Assert.AreEqual(expectedUser[0].Login, actual[0].Login);
            Assert.AreEqual(expectedUser[0].FirstName, actual[0].FirstName);
            Assert.AreEqual(expectedUser[0].LastName, actual[0].LastName);
            Assert.AreEqual(expectedUser[0].Email, actual[0].Email);
            Assert.AreEqual(expectedUser[0].DistinguishedName, actual[0].DistinguishedName);
            Assert.AreEqual(expectedUser[0].DNSHostname, actual[0].DNSHostname);
            Assert.AreEqual(expectedUser[0].GivenName, actual[0].GivenName);
            Assert.AreEqual(expectedUser[0].ProfilePath, actual[0].ProfilePath);
            Assert.AreEqual(expectedUser[0].SN, actual[0].SN);
            Assert.AreEqual(expectedUser[0].TelephoneNumber, actual[0].TelephoneNumber);
            Assert.AreEqual(expectedUser[0].UserPrincipalName, actual[0].UserPrincipalName);
            Assert.AreEqual(expectedUser[0].OfficeLocation, actual[0].OfficeLocation);

            Assert.AreEqual(expectedUser[1].Login, actual[1].Login);
            Assert.AreEqual(expectedUser[1].FirstName, actual[1].FirstName);
            Assert.AreEqual(expectedUser[1].LastName, actual[1].LastName);
            Assert.AreEqual(expectedUser[1].Email, actual[1].Email);
            Assert.AreEqual(expectedUser[1].DistinguishedName, actual[1].DistinguishedName);
            Assert.AreEqual(expectedUser[1].DNSHostname, actual[1].DNSHostname);
            Assert.AreEqual(expectedUser[1].GivenName, actual[1].GivenName);
            Assert.AreEqual(expectedUser[1].ProfilePath, actual[1].ProfilePath);
            Assert.AreEqual(expectedUser[1].SN, actual[1].SN);
            Assert.AreEqual(expectedUser[1].TelephoneNumber, actual[1].TelephoneNumber);
            Assert.AreEqual(expectedUser[1].UserPrincipalName, actual[1].UserPrincipalName);
            Assert.AreEqual(expectedUser[1].OfficeLocation, actual[1].OfficeLocation);
        }
        #endregion

        #region SearchUserByName tests - invalid arguments.
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SearchUserByName_SearchWithBothNullArgumentsShouldThrowException()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            var actual = target.SearchUserByName("", "");
        }

        [TestMethod]
        public void SearchUserByName_SearchWithBothNullArgumentsShouldThrowExceptionWithCorrectDebugInformation()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            try
            {
                var actual = target.SearchUserByName("", "");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "firstName, lastName");
                return;
            }

            Assert.Fail();
        }
        #endregion

        #region SearchUsersByLoginId tests - valid arguments.
        [TestMethod]
        public void SearchUsersByLoginId_ShouldReturnCorrectListOfADUser()
        {
            var expectedUser = new List<ADUser>()
                {
                    new ADUser()
                    {
                        Email = "FindEmail@test.com",
                        DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                        DNSHostname = "my Host Name",
                        FirstName = "Fred",
                        LastName = "Blogs",
                        GivenName = "Fred Blogs",
                        Login = "bfred",
                        ProfilePath = @"\\ServerName\SomeFolderOnServer",
                        SN = "sn information",
                        TelephoneNumber = "00 00 00 000",
                        UserPrincipalName = "bfred",
                        OfficeLocation = "ASTON"
                    }
                };

            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            var actual = target.SearchUsersByLoginId("77777777");

            Assert.AreEqual(expectedUser[0].Login, actual[0].Login);
            Assert.AreEqual(expectedUser[0].FirstName, actual[0].FirstName);
            Assert.AreEqual(expectedUser[0].LastName, actual[0].LastName);
            Assert.AreEqual(expectedUser[0].Email, actual[0].Email);
            Assert.AreEqual(expectedUser[0].DistinguishedName, actual[0].DistinguishedName);
            Assert.AreEqual(expectedUser[0].DNSHostname, actual[0].DNSHostname);
            Assert.AreEqual(expectedUser[0].GivenName, actual[0].GivenName);
            Assert.AreEqual(expectedUser[0].ProfilePath, actual[0].ProfilePath);
            Assert.AreEqual(expectedUser[0].SN, actual[0].SN);
            Assert.AreEqual(expectedUser[0].TelephoneNumber, actual[0].TelephoneNumber);
            Assert.AreEqual(expectedUser[0].UserPrincipalName, actual[0].UserPrincipalName);
            Assert.AreEqual(expectedUser[0].OfficeLocation, actual[0].OfficeLocation);

            Assert.IsTrue(expectedUser.Count == 1);
        }
        #endregion

        #region SearchUsersByLoginId tests - invalid arguments.
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SearchUsersByLoginId_ShouldThrowExceptionOnNullAccountName()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            target.SearchUsersByLoginId("");
        }

        [TestMethod]
        public void SearchUsersByLoginId_ShouldThrowExceptionOnNullAccountNameWithCorrectDebugInformation()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            try
            {
                target.SearchUsersByLoginId("");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "samAccountName");
                return;
            }

            Assert.Fail();
        }
        #endregion

        #region GetUsersInGroupMembership tests - valid arguments.
        [TestMethod]
        public void GetUsersInGroupMembership_ShouldReturnCorrectListOfADUser()
        {
            var expectedUser = new List<ADUser>()
                {
                    new ADUser()
                    {
                        Email = "FindEmail@test.com",
                        DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                        DNSHostname = "my Host Name",
                        FirstName = "Fred",
                        LastName = "Blogs",
                        GivenName = "Fred Blogs",
                        Login = "bfred",
                        ProfilePath = @"\\ServerName\SomeFolderOnServer",
                        SN = "sn information",
                        TelephoneNumber = "00 00 00 000",
                        UserPrincipalName = "bfred",
                        OfficeLocation = "ASTON"
                    },
                    new ADUser()
                    {
                        Email = "FindEmail1@test.com",
                        DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy",
                        DNSHostname = "my Host Name 2",
                        FirstName = "Fred 2",
                        LastName = "Blogs 2",
                        GivenName = "Fred Blogs 2",
                        Login = "bfred2",
                        ProfilePath = @"\\ServerName\SomeFolderOnServer2",
                        SN = "sn information2",
                        TelephoneNumber = "00 00 00 002",
                        UserPrincipalName = "bfred2",
                        OfficeLocation = "ASTON"
                    }
                };

            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            var actual = target.GetUsersInGroupMembership("group");

            Assert.AreEqual(expectedUser[0].Login, actual[0].Login);
            Assert.AreEqual(expectedUser[0].FirstName, actual[0].FirstName);
            Assert.AreEqual(expectedUser[0].LastName, actual[0].LastName);
            Assert.AreEqual(expectedUser[0].Email, actual[0].Email);
            Assert.AreEqual(expectedUser[0].DistinguishedName, actual[0].DistinguishedName);
            Assert.AreEqual(expectedUser[0].DNSHostname, actual[0].DNSHostname);
            Assert.AreEqual(expectedUser[0].GivenName, actual[0].GivenName);
            Assert.AreEqual(expectedUser[0].ProfilePath, actual[0].ProfilePath);
            Assert.AreEqual(expectedUser[0].SN, actual[0].SN);
            Assert.AreEqual(expectedUser[0].TelephoneNumber, actual[0].TelephoneNumber);
            Assert.AreEqual(expectedUser[0].UserPrincipalName, actual[0].UserPrincipalName);
            Assert.AreEqual(expectedUser[0].OfficeLocation, actual[0].OfficeLocation);

            Assert.AreEqual(expectedUser[1].Login, actual[1].Login);
            Assert.AreEqual(expectedUser[1].FirstName, actual[1].FirstName);
            Assert.AreEqual(expectedUser[1].LastName, actual[1].LastName);
            Assert.AreEqual(expectedUser[1].Email, actual[1].Email);
            Assert.AreEqual(expectedUser[1].DistinguishedName, actual[1].DistinguishedName);
            Assert.AreEqual(expectedUser[1].DNSHostname, actual[1].DNSHostname);
            Assert.AreEqual(expectedUser[1].GivenName, actual[1].GivenName);
            Assert.AreEqual(expectedUser[1].ProfilePath, actual[1].ProfilePath);
            Assert.AreEqual(expectedUser[1].SN, actual[1].SN);
            Assert.AreEqual(expectedUser[1].TelephoneNumber, actual[1].TelephoneNumber);
            Assert.AreEqual(expectedUser[1].UserPrincipalName, actual[1].UserPrincipalName);
            Assert.AreEqual(expectedUser[1].OfficeLocation, actual[1].OfficeLocation);
        }
        #endregion

        #region GetUsersInGroupMembership tests - invalid arguments.
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetUsersInGroupMembership_ShouldThrowExceptionOnNullGroupName()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            target.GetUsersInGroupMembership("");
        }

        [TestMethod]
        public void GetUsersInGroupMembership_ShouldThrowExceptionOnNullGroupNameWithCorrectDebugInformation()
        {
            IMyPrincipalSearcher searcher = new FakePrincipalSearcher();
            ActiveDirectoryHelper target = new ActiveDirectoryHelper(searcher);

            try
            {
                target.GetUsersInGroupMembership("");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "groupName");
                return;
            }

            Assert.Fail();
        }
        #endregion
    }
}