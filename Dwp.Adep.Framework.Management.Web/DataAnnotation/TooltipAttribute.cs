using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dwp.Adep.Framework.Management.Web.DataAnnotation
{
    public sealed class TooltipAttribute : Attribute, IMetadataAware
    {
        private string tooltip;
        private Type resourceType;

        public TooltipAttribute(string tooltipName)
        {
            tooltip = tooltipName;
        }

        //Tooltip is mandatory. If no resource type is supplied, then this is used as the tooltip. Otherwise, a resource string is used that has the same name
        public string Tooltip
        {
            get
            {
                if (resourceType != null)
                {
                    return resourceType.GetProperty(tooltip).GetValue(null, null).ToString();
                }
                else
                {
                    return tooltip;
                }
            }
        }
        //Resource Type
        public Type ResourceType
        {
            get { return resourceType; }
            set { resourceType = value; }
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["Tooltip"] = this.Tooltip;
        }

    }
}