using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Management.Web.Models;
using System.ComponentModel.DataAnnotations;
using Dwp.Adep.Framework.Management.ResourceLibrary;
using Dwp.Adep.Framework.Management.Web.Helpers;

namespace Dwp.Adep.Framework.Management.Web.ViewModels
{
    public class ApplicationOrganisationAdminVM : IValidatableObject
    {
        /// <summary>
        /// The organisation being edited
        /// </summary>
        public OrganisationModel OrganisationItem { get; set; }

        /// <summary>
        /// Flag indicating if the organisation has child organisations
        /// </summary>
        public int MaximumHopsToChildOrganisation { get; set; }

        /// <summary>
        /// The organisation parent
        /// </summary>
        [Display(Name = "LABEL_PARENT_ORGANISATION", ResourceType = typeof(FixedResources))]
        [Required]
        public Guid ParentOrganisationCode { get; set; }


        /// <summary>
        /// The root node for application
        /// </summary>
        public OrganisationModel RootNodeOrganisation { get; set; }

        /// <summary>
        /// The organisations that can be set to parent org
        /// </summary>
        public List<OrganisationByTypeVM> OrganisationsByTypesList { get; set; }

        /// <summary>
        /// All types in the application organisation type group
        /// </summary>
        public List<OrganisationTypeModel> AllTypesForApplication { get; set; }

        /// <summary>
        /// Flag used to mark the view values as having changed
        /// </summary>
        public bool IsViewDirty { get; set; }

        /// <summary>
        /// Flag used to ensure the user confirms a exit action
        /// </summary>
        public string IsExitConfirmed { get; set; }

        /// <summary>
        /// Property for holding messages to be displayed to the user
        /// </summary>
        public string Message { get; set; }        
        public string IsNewConfirmed { get; set; }
        
        public ApplicationOrganisationAccessContext AccessContext { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (OrganisationItem.OrganisationTypeCode != Guid.Empty)
            {
                if (AllTypesForApplication.Single(x => x.Code == OrganisationItem.OrganisationTypeCode).LevelNumber + MaximumHopsToChildOrganisation > AllTypesForApplication.Count)
                {
                    results.Add(new ValidationResult("A move to this organisation type would make the children fall out of the hierarchy", new string[1] { "OrganisationItem.OrganisationTypeCode" }));
                }
            }
            if (ParentOrganisationCode == Guid.Empty && OrganisationItem.Code != Guid.Empty) //Add Validation error if updating a case
            {
                results.Add(new ValidationResult("Cannot update Organisation without a Parent Organisation", new string[1] { "OrganisationItem.OrganisationTypeCode" }));
            }
            return results;
        }
    }

    public enum ApplicationOrganisationAccessContext
    {
        Create,
        View,
        Edit
    }

}