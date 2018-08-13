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
    /// Class containing service behaviour for Title
    /// </summary>
    public partial class AdminService
    {
        #region Behaviour for Title
    
            #region Create
    
    		/// <summary>
            /// Create a Title
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public TitleVMDC CreateTitle(string currentUser, string user, string appID, string overrideID, TitleDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    	
    			// Create repository
                Repository<Title> dataRepository = new Repository<Title>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return CreateTitle(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            ///  Create a Title
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public TitleVMDC CreateTitle(string currentUser, string user, string appID, string overrideID, TitleDC dc, IRepository<Title> dataRepository, IUnitOfWork uow)
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
    					// Create a new ID for the Title item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        Title destination = Mapper.Map<TitleDC, Title>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
                    }
    				
    				// Create aggregate data contract
    				TitleVMDC returnObject = new TitleVMDC();
    				
    				// Add new item to aggregate
    				returnObject.TitleItem = dc;
    				
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
    
            #region GetTitle
    
    		/// <summary>
        	/// Retrieve a Title with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public TitleVMDC GetTitle(string currentUser, string user, string appID, string overrideID, string code)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    
    			// Create repository
                IRepository<Title> dataRepository = new Repository<Title>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    
    			// Call overload with injected objects
                return GetTitle(currentUser, user, appID, overrideID, code, uow, dataRepository
    			);
            }
    
    		/// <summary>
            /// Retrieve a Title with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public TitleVMDC GetTitle(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<Title> dataRepository
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
    				
    					TitleDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific Title
    	                    Title dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = Mapper.Map<Title, TitleDC>(dataEntity);
    					}
    
    
    
        				// Create aggregate contract
                        TitleVMDC returnObject = new TitleVMDC();
    
                        returnObject.TitleItem = destination;
                        
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
            public List<TitleDC> GetAllTitle(string currentUser, string user, string appID, string overrideID, bool includeInActive)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    
    			// Create repository
                Repository<Title> dataRepository = new Repository<Title>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<Title> specification = new Specification<Title>();
    
    			// Call overload with injected objects
                return GetAllTitle(currentUser, user, appID, overrideID, includeInActive, specification, dataRepository, uow);
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
            public List<TitleDC> GetAllTitle(string currentUser, string user, string appID, string overrideID,  bool includeInActive, ISpecification<Title> specification, IRepository<Title> dataRepository, IUnitOfWork uow)
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
                            ISpecification<Title> isActiveSpecification = new Specification<Title>(x => x.IsActive == true);
                            specification = specification.And(isActiveSpecification);
                        }
    
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<Title, Object>> sortExpression = x => x.Description;
    					
                        // Find all items that satisfy the specification created above.
                        IEnumerable<Title> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<TitleDC> destinations = Mapper.Map<IEnumerable<Title>, List<TitleDC>>(dataEntities);
    
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
            /// Search for Title items
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
    		public TitleSearchVMDC SearchTitle(string currentUser, string user, string appID, string overrideID, TitleSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork();
    
    			// Create repository
                IRepository<Title> dataRepository = new Repository<Title>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<Title> specification = new Specification<Title>();
    
    			// Call overload with injected objects
                return SearchTitle(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, includeInActive, specification, dataRepository, uow);
    		}
    
    		/// <summary>
            /// Search for Title items
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
    		public TitleSearchVMDC SearchTitle(string currentUser, string user, string appID, string overrideID, TitleSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive,
    		ISpecification<Title> specification, IRepository<Title> dataRepository, IUnitOfWork uow)
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
                            EvaluateTitleSearchCriteria(searchCriteria, ref specification);
                        }
    
                        if (!includeInActive)
                        {
                            ISpecification<Title> isActiveSpecification = new Specification<Title>(x => x.IsActive == true);
                            specification = specification.And(isActiveSpecification);
                        }
    
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<Title, Object>> sortExpression = x => x.Description;
    					
    				    // Find all items that satisfy the specification created above.
                        IEnumerable<Title> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Find(specification).Count();
    
    					TitleSearchVMDC results = new TitleSearchVMDC();
    
    					// Convert to data contracts
                        List<TitleSearchMatchDC> destinations = Mapper.Map<IEnumerable<Title>, List<TitleSearchMatchDC>>(dataEntities);
    
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
    		
    		// Partial method for evaluation of Title search criteria
            partial void EvaluateTitleSearchCriteria(TitleSearchCriteriaDC searchCriteria, ref ISpecification<Title> specification);
            #endregion
    
            #region Update
    
    		/// <summary>
            /// Update a Title
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public TitleVMDC UpdateTitle(string currentUser, string user, string appID, string overrideID, TitleDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    			
    			// Create repository
                IRepository<Title> dataRepository = new Repository<Title>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return UpdateTitle(currentUser, user, appID, overrideID, dc, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a Title
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public TitleVMDC UpdateTitle(string currentUser, string user, string appID, string overrideID, TitleDC dc, IRepository<Title> dataRepository, IUnitOfWork uow)
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
                        Title destination = Mapper.Map<TitleDC, Title>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
                    }
    				
    				// Create new data contract to return
    				TitleVMDC returnObject = new TitleVMDC();
    				
    				// Add new item to datacontract
    				returnObject.TitleItem = dc;
    				
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
            /// Delete a Title
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteTitle(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork();
    
    			// Create repository
                IRepository<Title> dataRepository = new Repository<Title>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                DeleteTitle(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow);
            }
    
    		/// <summary>
            /// Update a Title
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteTitle(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<Title> dataRepository, IUnitOfWork uow)
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
                        Title dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
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