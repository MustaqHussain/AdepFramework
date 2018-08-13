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
    /// Class containing service behaviour for LastUpdate
    /// </summary>
    public partial class AdminService
    {
        #region Behaviour for LastUpdate
    
            #region Create
    
    		/// <summary>
            /// Create a LastUpdate
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public LastUpdateVMDC CreateLastUpdate(string currentUser, string user, string appID, string overrideID, LastUpdateDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    	
    			// Create repository
                Repository<LastUpdate> dataRepository = new Repository<LastUpdate>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return CreateLastUpdate(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            ///  Create a LastUpdate
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public LastUpdateVMDC CreateLastUpdate(string currentUser, string user, string appID, string overrideID, LastUpdateDC dc, IRepository<LastUpdate> dataRepository, IUnitOfWork uow)
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
    					// Create a new ID for the LastUpdate item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        LastUpdate destination = Mapper.Map<LastUpdateDC, LastUpdate>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
                    }
    				
    				// Create aggregate data contract
    				LastUpdateVMDC returnObject = new LastUpdateVMDC();
    				
    				// Add new item to aggregate
    				returnObject.LastUpdateItem = dc;
    				
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
    
            #region GetLastUpdate
    
    		/// <summary>
        	/// Retrieve a LastUpdate with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public LastUpdateVMDC GetLastUpdate(string currentUser, string user, string appID, string overrideID, string code)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    
    			// Create repository
                IRepository<LastUpdate> dataRepository = new Repository<LastUpdate>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    
    			// Call overload with injected objects
                return GetLastUpdate(currentUser, user, appID, overrideID, code, uow, dataRepository
    			);
            }
    
    		/// <summary>
            /// Retrieve a LastUpdate with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public LastUpdateVMDC GetLastUpdate(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<LastUpdate> dataRepository
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
    				
    					LastUpdateDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific LastUpdate
    	                    LastUpdate dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = Mapper.Map<LastUpdate, LastUpdateDC>(dataEntity);
    					}
    
    
    
        				// Create aggregate contract
                        LastUpdateVMDC returnObject = new LastUpdateVMDC();
    
                        returnObject.LastUpdateItem = destination;
                        
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
    		/// <param name="includeInActive"></param>
            /// <returns></returns>
            public List<LastUpdateDC> GetAllLastUpdate(string currentUser, string user, string appID, string overrideID, bool includeInActive)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    
    			// Create repository
                Repository<LastUpdate> dataRepository = new Repository<LastUpdate>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<LastUpdate> specification = new Specification<LastUpdate>();
    
    			// Call overload with injected objects
                return GetAllLastUpdate(currentUser, user, appID, overrideID, includeInActive, specification, dataRepository, uow);
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
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public List<LastUpdateDC> GetAllLastUpdate(string currentUser, string user, string appID, string overrideID,  bool includeInActive, ISpecification<LastUpdate> specification, IRepository<LastUpdate> dataRepository, IUnitOfWork uow)
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
    					// Build specification
                        if (!includeInActive)
                        {
                            ISpecification<LastUpdate> isActiveSpecification = new Specification<LastUpdate>(x => x.IsActive == true);
                            specification = specification.And(isActiveSpecification);
                        }
    
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<LastUpdate, Object>> sortExpression = x => x.DateLastUpdate;
    					
                        // Find all items that satisfy the specification created above.
                        IEnumerable<LastUpdate> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<LastUpdateDC> destinations = Mapper.Map<IEnumerable<LastUpdate>, List<LastUpdateDC>>(dataEntities);
    
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
            /// Search for LastUpdate items
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="searchCriteria"></param>
            /// <param name="page"></param>
            /// <param name="pageSize"></param>
            /// <param name="includeInActive"></param>
            /// <returns></returns>
    		public LastUpdateSearchVMDC SearchLastUpdate(string currentUser, string user, string appID, string overrideID, LastUpdateSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork();
    
    			// Create repository
                IRepository<LastUpdate> dataRepository = new Repository<LastUpdate>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<LastUpdate> specification = new Specification<LastUpdate>();
    
    			// Call overload with injected objects
                return SearchLastUpdate(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, includeInActive, specification, dataRepository, uow);
    		}
    
    		/// <summary>
            /// Search for LastUpdate items
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="searchCriteria"></param>
            /// <param name="page"></param>
            /// <param name="pageSize"></param>
            /// <param name="includeInActive"></param>
    		/// <param name="specification"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
    		public LastUpdateSearchVMDC SearchLastUpdate(string currentUser, string user, string appID, string overrideID, LastUpdateSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive,
    		ISpecification<LastUpdate> specification, IRepository<LastUpdate> dataRepository, IUnitOfWork uow)
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
                            EvaluateLastUpdateSearchCriteria(searchCriteria, ref specification);
                        }
    
                        if (!includeInActive)
                        {
                            ISpecification<LastUpdate> isActiveSpecification = new Specification<LastUpdate>(x => x.IsActive == true);
                            specification = specification.And(isActiveSpecification);
                        }
    
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<LastUpdate, Object>> sortExpression = x => x.DateLastUpdate;
    					
    				    // Find all items that satisfy the specification created above.
                        IEnumerable<LastUpdate> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Find(specification).Count();
    
    					LastUpdateSearchVMDC results = new LastUpdateSearchVMDC();
    
    					// Convert to data contracts
                        List<LastUpdateSearchMatchDC> destinations = Mapper.Map<IEnumerable<LastUpdate>, List<LastUpdateSearchMatchDC>>(dataEntities);
    
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
    		
    		// Partial method for evaluation of LastUpdate search criteria
            partial void EvaluateLastUpdateSearchCriteria(LastUpdateSearchCriteriaDC searchCriteria, ref ISpecification<LastUpdate> specification);
            #endregion
    
            #region Update
    
    		/// <summary>
            /// Update a LastUpdate
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public LastUpdateVMDC UpdateLastUpdate(string currentUser, string user, string appID, string overrideID, LastUpdateDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    			
    			// Create repository
                IRepository<LastUpdate> dataRepository = new Repository<LastUpdate>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return UpdateLastUpdate(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a LastUpdate
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public LastUpdateVMDC UpdateLastUpdate(string currentUser, string user, string appID, string overrideID, LastUpdateDC dc, IRepository<LastUpdate> dataRepository, IUnitOfWork uow)
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
                        LastUpdate destination = Mapper.Map<LastUpdateDC, LastUpdate>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
                    }
    				
    				// Create new data contract to return
    				LastUpdateVMDC returnObject = new LastUpdateVMDC();
    				
    				// Add new item to datacontract
    				returnObject.LastUpdateItem = dc;
    				
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
            /// Delete a LastUpdate
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteLastUpdate(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    
    			// Create repository
                IRepository<LastUpdate> dataRepository = new Repository<LastUpdate>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                DeleteLastUpdate(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a LastUpdate
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteLastUpdate(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<LastUpdate> dataRepository, IUnitOfWork uow)
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
                        LastUpdate dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
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