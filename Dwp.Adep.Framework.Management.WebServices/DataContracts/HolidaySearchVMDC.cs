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
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public partial class HolidaySearchVMDC
    {
    	[DataMember]
        public HolidaySearchCriteriaDC SearchCriteria { get; set;}
    
    	[DataMember]
        public List<HolidaySearchMatchDC> MatchList { get; set;}
    
        [DataMember]
        public int RecordCount { get; set; }
    
    }
}
