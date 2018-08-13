using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.AD
{
    public class FakePrincipalSearcher : IMyPrincipalSearcher
    {
        public List<string> FindAll(string firstName, string lastName)
        {
            List<string> emailAddresses = new List<string>();
            emailAddresses.Add("FindAll@test.com");

            return emailAddresses;
        }

        public ADUser FindEmail(string samAccountName)
        {
            return new ADUser() { Email = "FindEmail@test.com" };
        }

        public ADUser SearchByLoginId(string samAccountName)
        {
            return new ADUser() { Email = "FindEmail@test.com", DistinguishedName = "xxx,xxxxx,xxx123456789xxxxlocation,yyyy", DNSHostname = "my Host Name", FirstName = "Fred", LastName = "Blogs", GivenName = "Fred Blogs", Login = "bfred", ProfilePath = @"\\ServerName\SomeFolderOnServer", SN = "sn information", TelephoneNumber = "00 00 00 000", UserPrincipalName = "bfred", OfficeLocation = "ASTON" };
        }

        public List<ADUser> SearchByName(string firstName, string lastName)
        {
            return GetAdUsers();
        }
        
        public List<ADUser> GetUsersInGroupMembership(string groupName)
        {
            return GetAdUsers();
        }

        private List<ADUser> GetAdUsers()
        {
            return new List<ADUser>()
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
        }

        public List<ADUser> SearchUsersByLoginId(string samAccountName)
        {
            return GetAdUsers();
        }
    }
}