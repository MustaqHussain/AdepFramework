using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Dwp.Adep.Framework.Management.IoC.ServiceLocation;
using Dwp.Adep.Framework.Management.DataServices;
using Dwp.Adep.Framework.Management.DataServices.Models;

namespace Dwp.Adep.Framework.Management.WebServices.Tests
{
    public class BootStrapper
    {
        public static void InitializeIoc()
        {
            // Call this method at App level e.g. Global.asax to ensure object referenced for app lifetime.
            SimpleServiceLocator.SetServiceLocatorProvider(new UnityServiceLocator());

            // UnitOfWork
            // SimpleServiceLocator.Instance.RegisterWithConstructorParameters<IUnitOfWork, UnitOfWork>("ManagementConsole", new object[] { "ManagementConsole" });
            SimpleServiceLocator.Instance.Register(typeof(IUnitOfWork), typeof(UnitOfWork));

            // Register Context
            SimpleServiceLocator.Instance.RegisterWithConstructorParameters(typeof(IObjectContext), typeof(AdepDatabaseEntities));

            //Register Generic Repositories
            SimpleServiceLocator.Instance.Register(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
