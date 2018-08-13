using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Dwp.Adep.Framework.Management.IoC.ServiceLocation;
using Dwp.Adep.Framework.Management.DataServices;
using Dwp.Adep.Framework.Management.DataServices.Models;

namespace Dwp.Adep.Framework.Management.WebServices
{
    public class BootStrapper
    {
        private static object _lockObject = new object();

        public static void InitializeIoc()
        {
            // Outside "if" to reduce contention
            if (null == SimpleServiceLocator.Instance)
            {
                // Only one thread to execute this code to determine if the servicelocator has alerady been created.
                lock (_lockObject)
                {
                    if (null == SimpleServiceLocator.Instance)
                    {
                        SimpleServiceLocator.SetServiceLocatorProvider(new UnityServiceLocator());

                        // UnitOfWork
                        // SimpleServiceLocator.Instance.RegisterWithConstructorParameters<IUnitOfWork, UnitOfWork>("ManagementConsole", new object[] { "ManagementConsole" });
                        SimpleServiceLocator.Instance.Register(typeof(IUnitOfWork), typeof(UnitOfWork));

                        // Register Context
                        SimpleServiceLocator.Instance.RegisterWithConstructorParameters(typeof(IObjectContext), typeof(AdepDatabaseEntities));
                        SimpleServiceLocator.Instance.RegisterWithConstructorParameters<IObjectContext, AdepDatabaseEntities>("AdepDatabaseEntities", new object[0]);

                        //Register Generic Repositories
                        SimpleServiceLocator.Instance.Register(typeof(IRepository<>), typeof(Repository<>));
                    }
                }
            }
        }
    }
}
