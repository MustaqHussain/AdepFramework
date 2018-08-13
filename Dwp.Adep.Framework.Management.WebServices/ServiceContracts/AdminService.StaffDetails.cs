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
    /// Class containing service behaviour for StaffDetails
    /// </summary>
    public partial class AdminService
    {
        #region Behaviour for StaffDetails
    
            #region Create
    
    		/// <summary>
            /// Create a StaffDetails
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public StaffDetailsVMDC CreateStaffDetails(string currentUser, string user, string appID, string overrideID, StaffDetailsDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                Repository<StaffDetails> dataRepository = new Repository<StaffDetails>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return CreateStaffDetails(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            ///  Create a StaffDetails
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public StaffDetailsVMDC CreateStaffDetails(string currentUser, string user, string appID, string overrideID, StaffDetailsDC dc, IRepository<StaffDetails> dataRepository, IUnitOfWork uow)
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
    					// Create a new ID for the StaffDetails item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        StaffDetails destination = Mapper.Map<StaffDetailsDC, StaffDetails>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
    
    					dc = Mapper.Map<StaffDetails, StaffDetailsDC>(destination);
                    }
    				
    				// Create aggregate data contract
    				StaffDetailsVMDC returnObject = new StaffDetailsVMDC();
    				
    				// Add new item to aggregate
    				returnObject.StaffDetailsItem = dc;
    				
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
    
            #region GetStaffDetails
    
    		/// <summary>
        	/// Retrieve a StaffDetails with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public StaffDetailsVMDC GetStaffDetails(string currentUser, string user, string appID, string overrideID, string code)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<StaffDetails> dataRepository = new Repository<StaffDetails>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    			IRepository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			IRepository<StaffOffices> staffOfficeRepository = new Repository<StaffOffices>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return GetStaffDetails(currentUser, user, appID, overrideID, code, uow, dataRepository
    			, staffRepository
    			, staffOfficeRepository
    			);
            }
    
    		/// <summary>
            /// Retrieve a StaffDetails with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public StaffDetailsVMDC GetStaffDetails(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<StaffDetails> dataRepository
    			,IRepository<Staff> staffRepository
    			,IRepository<StaffOffices> staffOfficeRepository
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
    				
    					StaffDetailsDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific StaffDetails
    	                    StaffDetails dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = Mapper.Map<StaffDetails, StaffDetailsDC>(dataEntity);
    					}
    
    					IEnumerable<Staff> staffList = staffRepository.GetAll(x => new {x.StaffNumber});
    					IEnumerable<StaffOffices> staffOfficeList = staffOfficeRepository.GetAll(x => new {x.Name});
    
    					List<StaffDC> staffDestinationList = Mapper.Map<List<StaffDC>>(staffList);
    					List<StaffOfficesDC> staffOfficeDestinationList = Mapper.Map<List<StaffOfficesDC>>(staffOfficeList);
    
        				// Create aggregate contract
                        StaffDetailsVMDC returnObject = new StaffDetailsVMDC();
    
                        returnObject.StaffDetailsItem = destination;
    					returnObject.StaffList = staffDestinationList;
    					returnObject.StaffOfficeList = staffOfficeDestinationList;
                        
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
            public List<StaffDetailsDC> GetAllStaffDetails(string currentUser, string user, string appID, string overrideID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                IRepository<StaffDetails> dataRepository = new Repository<StaffDetails>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<StaffDetails> specification = new Specification<StaffDetails>();
    
                return GetAllStaffDetails(currentUser, user, appID, overrideID, specification, dataRepository, uow);
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
            public List<StaffDetailsDC> GetAllStaffDetails(string currentUser, string user, string appID, string overrideID, ISpecification<StaffDetails> specification, IRepository<StaffDetails> dataRepository, IUnitOfWork uow)
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
    					System.Linq.Expressions.Expression<Func<StaffDetails, Object>> sortExpression = x => x.Section;
    
                        // Find all items that satisfy the specification created above.
                        IEnumerable<StaffDetails> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<StaffDetailsDC> destinations = Mapper.Map<IEnumerable<StaffDetails>, List<StaffDetailsDC>>(dataEntities);
    
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
    		public StaffDetailsSearchVMDC SearchStaffDetails(string currentUser, string user, string appID, string overrideID, StaffDetailsSearchCriteriaDC searchCriteria, int page, int pageSize)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<StaffDetails> dataRepository = new Repository<StaffDetails>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<StaffDetails> specification = new Specification<StaffDetails>();
    
    			// Call overload with injected objects
                return SearchStaffDetails(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, specification, dataRepository, uow);
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
    		public StaffDetailsSearchVMDC SearchStaffDetails(string currentUser, string user, string appID, string overrideID, StaffDetailsSearchCriteriaDC searchCriteria, int page, int pageSize, 
    		ISpecification<StaffDetails> specification, IRepository<StaffDetails> dataRepository, IUnitOfWork uow)
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
    					System.Linq.Expressions.Expression<Func<StaffDetails, Object>> sortExpression = x => x.Section;
    
    				    // Find all items that satisfy the specification created above.
                        IEnumerable<StaffDetails> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Find(specification).Count();
    
    					StaffDetailsSearchVMDC results = new StaffDetailsSearchVMDC();
    
    					// Convert to data contracts
                        List<StaffDetailsSearchMatchDC> destinations = Mapper.Map<IEnumerable<StaffDetails>, List<StaffDetailsSearchMatchDC>>(dataEntities);
    
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
            partial void EvaluateSearchCriteria(StaffDetailsSearchCriteriaDC searchCriteria, ref ISpecification<StaffDetails> specification);
            #endregion
    
            #region Update
    
    		/// <summary>
            /// Update a StaffDetails
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public StaffDetailsVMDC UpdateStaffDetails(string currentUser, string user, string appID, string overrideID, StaffDetailsDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    			
    			// Create repository
                IRepository<StaffDetails> dataRepository = new Repository<StaffDetails>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return UpdateStaffDetails(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a StaffDetails
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public StaffDetailsVMDC UpdateStaffDetails(string currentUser, string user, string appID, string overrideID, StaffDetailsDC dc, IRepository<StaffDetails> dataRepository, IUnitOfWork uow)
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
                        StaffDetails destination = Mapper.Map<StaffDetailsDC, StaffDetails>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
    
    					dc = Mapper.Map<StaffDetails, StaffDetailsDC>(destination);
                    }
    				
    				// Create new data contract to return
    				StaffDetailsVMDC returnObject = new StaffDetailsVMDC();
    				
    				// Add new item to datacontract
    				returnObject.StaffDetailsItem = dc;
    				
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
            /// Delete a StaffDetails
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteStaffDetails(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<StaffDetails> dataRepository = new Repository<StaffDetails>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                DeleteStaffDetails(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a StaffDetails
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteStaffDetails(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<StaffDetails> dataRepository, IUnitOfWork uow)
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
                        StaffDetails dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
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