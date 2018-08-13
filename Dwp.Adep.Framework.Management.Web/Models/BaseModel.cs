using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Dwp.Adep.Framework.Management.Web.Models
{
    [Serializable]
    public class BaseModel
    {
        /// <summary>
        /// Method to compare all the properties to a similarly typed object. Note deep linking is NOT implemented.
        /// </summary>
        /// <typeparam name="T">The type of the </typeparam>
        /// <param name="self"></param>
        /// <param name="to"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public bool PublicInstancePropertiesEqual<T>(T to, params string[] ignore) where T : class
        {
            if (this != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(this, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);
                        if (selfValue != null)
                        {
                            if (!selfValue.Equals(toValue))
                                return false;
                        }
                        else if (toValue != null)
                            return false;
                    }
                }
                return true;
            }
            return this == to;
        } 

    }
    public static class BaseModelExtensions
    {
        public static T Clone<T>(this T obj) where T : BaseModel
        {
            if (obj == null) return null;
            System.Reflection.MethodInfo inst = obj.GetType().GetMethod("MemberwiseClone",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (inst != null)
                return (T)inst.Invoke(obj, null);
            else
                return null;
        } 

    }
}