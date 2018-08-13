//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using Dwp.Adep.Framework.Management.WebServices.Exceptions;
using Dwp.Adep.Framework.Management.DataServices;
using Dwp.Adep.Framework.Management.DataServices.Models;
using Dwp.Adep.Framework.Management.WebServices.ServiceContracts;

namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts
{
    /// <summary>
    /// Admin service
    /// Class containing service behaviour for StaffOffices
    /// </summary>
    public partial class AdminService
    {
        #region Behaviour for StaffOffices
    
            #region Create
    
    		/// <summary>
            /// Create a StaffOffices
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public StaffOfficesVMDC CreateStaffOffices(string currentUser, string user, string appID, string overrideID, StaffOfficesDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                Repository<StaffOffices> dataRepository = new Repository<StaffOffices>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return CreateStaffOffices(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            ///  Create a StaffOffices
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public StaffOfficesVMDC CreateStaffOffices(string currentUser, string user, string appID, string overrideID, StaffOfficesDC dc, IRepository<StaffOffices> dataRepository, IUnitOfWork uow)
            {
                try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (null == dc) throw new ArgumentOutOfRangeException("dc");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    
    				#endregion
    
                    using (uow)
                    {
    					// Create a new ID for the StaffOffices item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        StaffOffices destination = Mapper.Map<StaffOfficesDC, StaffOffices>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
    
    					dc = Mapper.Map<StaffOffices, StaffOfficesDC>(destination);
                    }
    				
    				// Create aggregate data contract
    				StaffOfficesVMDC returnObject = new StaffOfficesVMDC();
    				
    				// Add new item to aggregate
    				returnObject.StaffOfficesItem = dc;
    				
    				return returnObject;
                }
                catch (Exception e)
                {
                    //Prevent exception from propogating across the service interface
                    ExceptionManager.ShieldException(e);
    				
    				return null;
                }
            }
    
            #endregion
    
            #region GetStaffOffices
    
    		/// <summary>
        	/// Retrieve a StaffOffices with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public StaffOfficesVMDC GetStaffOffices(string currentUser, string user, string appID, string overrideID, string code)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<StaffOffices> dataRepository = new Repository<StaffOffices>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    
    			// Call overload with injected objects
                return GetStaffOffices(currentUser, user, appID, overrideID, code, uow, dataRepository
    			);
            }
    
    		/// <summary>
            /// Retrieve a StaffOffices with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public StaffOfficesVMDC GetStaffOffices(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<StaffOffices> dataRepository
    			)
    		
            {
                try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    
    				#endregion
    
                    using (uow)
                    {
    				
    					StaffOfficesDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific StaffOffices
    	                    StaffOffices dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = Mapper.Map<StaffOffices, StaffOfficesDC>(dataEntity);
    					}
    
    
    
        				// Create aggregate contract
                        StaffOfficesVMDC returnObject = new StaffOfficesVMDC();
    
                        returnObject.StaffOfficesItem = destination;
                        
    					return returnObject;
                    }
                }
                catch (Exception e)
                {
                    //Prevent exception from propogating across the service interface
                    ExceptionManager.ShieldException(e);
    
                    return null;
                }
            }
    
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <returns></returns>
            public List<StaffOfficesDC> GetAllStaffOffices(string currentUser, string user, string appID, string overrideID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                IRepository<StaffOffices> dataRepository = new Repository<StaffOffices>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<StaffOffices> specification = new Specification<StaffOffices>();
    
                return GetAllStaffOffices(currentUser, user, appID, overrideID, specification, dataRepository, uow);
            }
    
    
    
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
    		/// <param name="specification"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public List<StaffOfficesDC> GetAllStaffOffices(string currentUser, string user, string appID, string overrideID, ISpecification<StaffOffices> specification, IRepository<StaffOffices> dataRepository, IUnitOfWork uow)
            {
                try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == specification) throw new ArgumentOutOfRangeException("specification");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    
    				#endregion
    
                    using (uow)
                    {
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<StaffOffices, Object>> sortExpression = x => x.Name;
    
                        // Find all items that satisfy the specification created above.
                        IEnumerable<StaffOffices> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<StaffOfficesDC> destinations = Mapper.Map<IEnumerable<StaffOffices>, List<StaffOfficesDC>>(dataEntities);
    
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
    
    		#endregion
    
    		#region Search
    
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="searchCriteria"></param>
            /// <param name="page"></param>
            /// <param name="pageSize"></param>
            /// <returns></returns>
    		public StaffOfficesSearchVMDC SearchStaffOffices(string currentUser, string user, string appID, string overrideID, StaffOfficesSearchCriteriaDC searchCriteria, int page, int pageSize)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<StaffOffices> dataRepository = new Repository<StaffOffices>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<StaffOffices> specification = new Specification<StaffOffices>();
    
    			// Call overload with injected objects
                return SearchStaffOffices(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, specification, dataRepository, uow);
    		}
    
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="searchCriteria"></param>
            /// <param name="page"></param>
            /// <param name="pageSize"></param>
    		/// <param name="specification"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
    		public StaffOfficesSearchVMDC SearchStaffOffices(string currentUser, string user, string appID, string overrideID, StaffOfficesSearchCriteriaDC searchCriteria, int page, int pageSize, 
    		ISpecification<StaffOffices> specification, IRepository<StaffOffices> dataRepository, IUnitOfWork uow)
            {
    		    try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == specification) throw new ArgumentOutOfRangeException("specification");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    
    				#endregion
    
                    using (uow)
                    {
                        // Evaluate search criteria if supplied
                        if (null != searchCriteria)
                        {
                            EvaluateSearchCriteria(searchCriteria, ref specification);
                        }
    					
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<StaffOffices, Object>> sortExpression = x => x.Name;
    
    				    // Find all items that satisfy the specification created above.
                        IEnumerable<StaffOffices> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Find(specification).Count();
    
    					StaffOfficesSearchVMDC results = new StaffOfficesSearchVMDC();
    
    					// Convert to data contracts
                        List<StaffOfficesSearchMatchDC> destinations = Mapper.Map<IEnumerable<StaffOffices>, List<StaffOfficesSearchMatchDC>>(dataEntities);
    
    					results.MatchList = destinations;
    					results.SearchCriteria = searchCriteria;
    					results.RecordCount = itemCount;
    
                        return results;
                    }
                }
                catch (Exception e)
                {
                    //Prevent exception from propogating across the service interface
                    ExceptionManager.ShieldException(e);
    
                    return null;
                }
    		}
    		
    		// Partial method for evaluation of search criteria
            partial void EvaluateSearchCriteria(StaffOfficesSearchCriteriaDC searchCriteria, ref ISpecification<StaffOffices> specification);
            #endregion
    
            #region Update
    
    		/// <summary>
            /// Update a StaffOffices
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public StaffOfficesVMDC UpdateStaffOffices(string currentUser, string user, string appID, string overrideID, StaffOfficesDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    			
    			// Create repository
                IRepository<StaffOffices> dataRepository = new Repository<StaffOffices>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return UpdateStaffOffices(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a StaffOffices
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public StaffOfficesVMDC UpdateStaffOffices(string currentUser, string user, string appID, string overrideID, StaffOfficesDC dc, IRepository<StaffOffices> dataRepository, IUnitOfWork uow)
            {
                try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (null == dc) throw new ArgumentOutOfRangeException("dc");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    
    				#endregion
    
                    using (uow)
                    {
    					// Map data contract to model
                        StaffOffices destination = Mapper.Map<StaffOfficesDC, StaffOffices>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
    
    					dc = Mapper.Map<StaffOffices, StaffOfficesDC>(destination);
                    }
    				
    				// Create new data contract to return
    				StaffOfficesVMDC returnObject = new StaffOfficesVMDC();
    				
    				// Add new item to datacontract
    				returnObject.StaffOfficesItem = dc;
    				
    				// Commit unit of work
    				return returnObject;
    				
                }
                catch (Exception e)
                {
                    //Prevent exception from propogating across the service interface
                    ExceptionManager.ShieldException(e);
    				
    				return null;
                }
            }
    
            #endregion
    
            #region Delete
    
    		/// <summary>
            /// Delete a StaffOffices
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteStaffOffices(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<StaffOffices> dataRepository = new Repository<StaffOffices>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                DeleteStaffOffices(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a StaffOffices
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteStaffOffices(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<StaffOffices> dataRepository, IUnitOfWork uow)
            {
                try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (string.IsNullOrEmpty(code)) throw new ArgumentOutOfRangeException("code");
    				if (string.IsNullOrEmpty(lockID)) throw new ArgumentOutOfRangeException("lockID");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    
    				#endregion
    
                    using (uow)
                    {
    					// Convert string to guid
                        Guid codeGuid = Guid.Parse(code);	
    					
    					// Find item based on ID
                        StaffOffices dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
    					// Delete the item
                        dataRepository.Delete(dataEntity);
    
    					// Commit unit of work
                        uow.Commit();
                    }
                }
                catch (Exception e)
                {
                    //Prevent exception from propogating across the service interface
                    ExceptionManager.ShieldException(e);
                }
            }
    
            #endregion

        #endregion
    }
}
