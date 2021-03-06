﻿//------------------------------------------------------------------------------
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
using System.ComponentModel.DataAnnotations;
using Dwp.Adep.Framework.Management.ResourceLibrary;
using DataAnnotationsExtensions;

namespace Dwp.Adep.Framework.Management.Web.Models
{
    [MetadataTypeAttribute(typeof(StaffSearchCriteriaModel.StaffSearchCriteriaModelMetadata))]
    public partial class StaffSearchCriteriaModel
    {
        public partial class StaffSearchCriteriaModelMetadata
        {
            [StringLength(8, ErrorMessageResourceName = "VAL_STAFFNUMBER", ErrorMessageResourceType = typeof(FixedResources))]
            [Integer(ErrorMessageResourceName = "VAL_STAFFNUMBER", ErrorMessageResourceType=typeof(FixedResources))]
            [Display(Name = "LABEL_STAFF_STAFFNUMBER", ResourceType = typeof(Resources))]
            public string StaffNumber { get; set; }

            [StringLength(35)]
            [Display(Name = "LABEL_STAFF_LASTNAME", ResourceType = typeof(Resources))]
            public string LastName { get; set; }

            [StringLength(35)]
            [Display(Name = "LABEL_STAFF_FIRSTNAME", ResourceType = typeof(Resources))]
            public string FirstName { get; set; }

            [Display(Name = "LABEL_STAFF_GRADECODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> GradeCode { get; set; }
        }
    }
}
