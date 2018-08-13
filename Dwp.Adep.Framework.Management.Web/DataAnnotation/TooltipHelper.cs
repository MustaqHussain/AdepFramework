using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace Dwp.Adep.Framework.Management.Web.DataAnnotation
{
    public static class TooltipHelper
    {
        public static HtmlString ToolTipFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var exp = (MemberExpression)expression.Body;

            DataAnnotationsModelMetadataProvider prov = new DataAnnotationsModelMetadataProvider();

            var metadataForProperty = prov.GetMetadataForProperty(null, exp.Expression.Type, exp.Member.Name);
            if (metadataForProperty.AdditionalValues.ContainsKey("Tooltip"))
            {
                return new HtmlString((string)metadataForProperty.AdditionalValues["Tooltip"]);
            }
            return new HtmlString("");
        }
    }
}