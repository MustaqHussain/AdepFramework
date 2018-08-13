using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using AutoMapper;
using Dwp.Adep.Framework.Management.WebServices.DataContracts;
using Dwp.Adep.Framework.Management.DataServices;
using Dwp.Adep.Framework.Management.WebServices.Exceptions;
using Dwp.Adep.Framework.Management.DataServices.Models;
using Dwp.Adep.Framework.Management.DataServices.Specification;


namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts
{
    public partial class AdminService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="includeInActive"></param>
        /// <returns></returns>
        public List<ApplicationDC> GetApplicationsWithStaffAdmin(string currentUser, string user, string appID, string overrideID, string[] userRoles, bool includeInActive)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repositories
            IRepository<Application> applicationRepository = new Repository<Application>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Create specification's for filtering
            ISpecification<Application> isStaffAdminSpecification = new IsStaffAdminSpecification(currentUser);

            // Call overload with injected objects
            return GetApplicationsWithStaffAdmin(currentUser, user, appID, overrideID, includeInActive, isStaffAdminSpecification, userRoles, applicationRepository, staffRepository, uow);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="includeInActive"></param>
        /// <param name="specification"></param>
        /// <param name="applicationRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public List<ApplicationDC> GetApplicationsWithStaffAdmin(string currentUser, string user, string appID, string overrideID, bool includeInActive, 
            ISpecification<Application> isStaffAdminSpecification, string[] userRoles, IRepository<Application> applicationRepository, IRepository<Staff> staffRepository, IUnitOfWork uow)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");

                if (null == applicationRepository) throw new ArgumentOutOfRangeException("applicationRepository");
                if (null == staffRepository) throw new ArgumentOutOfRangeException("staffRepository");
                if (null == isStaffAdminSpecification) throw new ArgumentOutOfRangeException("isStaffAdminSpecification");
                if (null == userRoles) throw new ArgumentOutOfRangeException("userRoles");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {

                    ISpecification<Application> allApplicationSpecification = new Specification<Application>();
                    if (includeInActive)
                    {
                       isStaffAdminSpecification =  isStaffAdminSpecification.And(new IsActiveSpecification<Application>());
                       allApplicationSpecification = allApplicationSpecification.And(new IsActiveSpecification<Application>());
                    }

                    // Set default sort expression
                    System.Linq.Expressions.Expression<Func<Application, Object>> sortExpression = x => x.Description; 
                  
                    IEnumerable<Application> dataEntities;

                    // Discover if current user is super admin                    
                    if(userRoles.Contains(FrameworkRoles.ADMIN))
                    {
                        dataEntities = applicationRepository.Find(allApplicationSpecification, sortExpression);
                    }
                    else
                    {
                        dataEntities = applicationRepository.Find(isStaffAdminSpecification, sortExpression);
                    }

                    // Convert to data contracts
                    List<ApplicationDC> destinations = Mapper.Map<IEnumerable<Application>, List<ApplicationDC>>(dataEntities);

                    return destinations;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);

                return null;
            }
        }
    }
}