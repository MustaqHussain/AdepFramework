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
    /// Class containing service behaviour for NonStandardHoliday
    /// </summary>
    public partial class AdminService
    {
        #region Behaviour for NonStandardHoliday
    
            #region Create
    
    		/// <summary>
            /// Create a NonStandardHoliday
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public NonStandardHolidayVMDC CreateNonStandardHoliday(string currentUser, string user, string appID, string overrideID, NonStandardHolidayDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                Repository<NonStandardHoliday> dataRepository = new Repository<NonStandardHoliday>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return CreateNonStandardHoliday(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            ///  Create a NonStandardHoliday
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public NonStandardHolidayVMDC CreateNonStandardHoliday(string currentUser, string user, string appID, string overrideID, NonStandardHolidayDC dc, IRepository<NonStandardHoliday> dataRepository, IUnitOfWork uow)
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
    					// Create a new ID for the NonStandardHoliday item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        NonStandardHoliday destination = Mapper.Map<NonStandardHolidayDC, NonStandardHoliday>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
    
    					dc = Mapper.Map<NonStandardHoliday, NonStandardHolidayDC>(destination);
                    }
    				
    				// Create aggregate data contract
    				NonStandardHolidayVMDC returnObject = new NonStandardHolidayVMDC();
    				
    				// Add new item to aggregate
    				returnObject.NonStandardHolidayItem = dc;
    				
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
    
            #region GetNonStandardHoliday
    
    		/// <summary>
        	/// Retrieve a NonStandardHoliday with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public NonStandardHolidayVMDC GetNonStandardHoliday(string currentUser, string user, string appID, string overrideID, string code)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<NonStandardHoliday> dataRepository = new Repository<NonStandardHoliday>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    
    			// Call overload with injected objects
                return GetNonStandardHoliday(currentUser, user, appID, overrideID, code, uow, dataRepository
    			);
            }
    
    		/// <summary>
            /// Retrieve a NonStandardHoliday with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public NonStandardHolidayVMDC GetNonStandardHoliday(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<NonStandardHoliday> dataRepository
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
    				
    					NonStandardHolidayDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific NonStandardHoliday
    	                    NonStandardHoliday dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = Mapper.Map<NonStandardHoliday, NonStandardHolidayDC>(dataEntity);
    					}
    
    
    
        				// Create aggregate contract
                        NonStandardHolidayVMDC returnObject = new NonStandardHolidayVMDC();
    
                        returnObject.NonStandardHolidayItem = destination;
                        
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
            public List<NonStandardHolidayDC> GetAllNonStandardHoliday(string currentUser, string user, string appID, string overrideID, bool includeInActive)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                Repository<NonStandardHoliday> dataRepository = new Repository<NonStandardHoliday>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<NonStandardHoliday> specification = new Specification<NonStandardHoliday>();
    
    			// Call overload with injected objects
                return GetAllNonStandardHoliday(currentUser, user, appID, overrideID, includeInActive, specification, dataRepository, uow);
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
            public List<NonStandardHolidayDC> GetAllNonStandardHoliday(string currentUser, string user, string appID, string overrideID,  bool includeInActive, ISpecification<NonStandardHoliday> specification, IRepository<NonStandardHoliday> dataRepository, IUnitOfWork uow)
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
                            ISpecification<NonStandardHoliday> isActiveSpecification = new Specification<NonStandardHoliday>(x => x.IsActive == true);
                            specification = specification.And(isActiveSpecification);
                        }
    
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<NonStandardHoliday, Object>> sortExpression = x => x.Description;
    					
                        // Find all items that satisfy the specification created above.
                        IEnumerable<NonStandardHoliday> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<NonStandardHolidayDC> destinations = Mapper.Map<IEnumerable<NonStandardHoliday>, List<NonStandardHolidayDC>>(dataEntities);
    
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
            /// Search for NonStandardHoliday items
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
    		public NonStandardHolidaySearchVMDC SearchNonStandardHoliday(string currentUser, string user, string appID, string overrideID, NonStandardHolidaySearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<NonStandardHoliday> dataRepository = new Repository<NonStandardHoliday>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<NonStandardHoliday> specification = new Specification<NonStandardHoliday>();
    
    			// Call overload with injected objects
                return SearchNonStandardHoliday(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, includeInActive, specification, dataRepository, uow);
    		}
    
    		/// <summary>
            /// Search for NonStandardHoliday items
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
    		public NonStandardHolidaySearchVMDC SearchNonStandardHoliday(string currentUser, string user, string appID, string overrideID, NonStandardHolidaySearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive,
    		ISpecification<NonStandardHoliday> specification, IRepository<NonStandardHoliday> dataRepository, IUnitOfWork uow)
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
                            EvaluateNonStandardHolidaySearchCriteria(searchCriteria, ref specification);
                        }
    
                        if (!includeInActive)
                        {
                            ISpecification<NonStandardHoliday> isActiveSpecification = new Specification<NonStandardHoliday>(x => x.IsActive == true);
                            specification = specification.And(isActiveSpecification);
                        }
    
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<NonStandardHoliday, DateTime>> sortExpression = x => x.Date;

                        //Sort holidays in descending order
                        bool isAscendingSort = false;

    				    // Find all items that satisfy the specification created above.
                        IEnumerable<NonStandardHoliday> dataEntities = dataRepository.Find<DateTime>(specification, sortExpression, isAscendingSort, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Count(specification);
    
    					NonStandardHolidaySearchVMDC results = new NonStandardHolidaySearchVMDC();
    
    					// Convert to data contracts
                        List<NonStandardHolidaySearchMatchDC> destinations = Mapper.Map<IEnumerable<NonStandardHoliday>, List<NonStandardHolidaySearchMatchDC>>(dataEntities);
    
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
    		
    		// Partial method for evaluation of NonStandardHoliday search criteria
            partial void EvaluateNonStandardHolidaySearchCriteria(NonStandardHolidaySearchCriteriaDC searchCriteria, ref ISpecification<NonStandardHoliday> specification);
            #endregion
    
            #region Update
    
    		/// <summary>
            /// Update a NonStandardHoliday
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public NonStandardHolidayVMDC UpdateNonStandardHoliday(string currentUser, string user, string appID, string overrideID, NonStandardHolidayDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    			
    			// Create repository
                IRepository<NonStandardHoliday> dataRepository = new Repository<NonStandardHoliday>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return UpdateNonStandardHoliday(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a NonStandardHoliday
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public NonStandardHolidayVMDC UpdateNonStandardHoliday(string currentUser, string user, string appID, string overrideID, NonStandardHolidayDC dc, IRepository<NonStandardHoliday> dataRepository, IUnitOfWork uow)
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
                        NonStandardHoliday destination = Mapper.Map<NonStandardHolidayDC, NonStandardHoliday>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
    
    					dc = Mapper.Map<NonStandardHoliday, NonStandardHolidayDC>(destination);
                    }
    				
    				// Create new data contract to return
    				NonStandardHolidayVMDC returnObject = new NonStandardHolidayVMDC();
    				
    				// Add new item to datacontract
    				returnObject.NonStandardHolidayItem = dc;
    				
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
            /// Delete a NonStandardHoliday
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteNonStandardHoliday(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<NonStandardHoliday> dataRepository = new Repository<NonStandardHoliday>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                DeleteNonStandardHoliday(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a NonStandardHoliday
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteNonStandardHoliday(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<NonStandardHoliday> dataRepository, IUnitOfWork uow)
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
                        NonStandardHoliday dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
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
