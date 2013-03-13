using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Umbraco.Core.Models;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper
{
    public class ValueBuilder
    {
        private readonly IPublishedContentProperty nodeProperty;
        private readonly PropertyInfo propertyInfo;
        private readonly bool isPreValue;

        public ValueBuilder(IPublishedContentProperty nodeProperty, PropertyInfo propertyInfo, bool isPreValue)
        {
            this.nodeProperty = nodeProperty;
            this.propertyInfo = propertyInfo;
            this.isPreValue = isPreValue;
        }

        public object GetValue()
        {
            if (nodeProperty == null || nodeProperty.Value == null || string.IsNullOrEmpty(nodeProperty.Value.ToString()))
            {
                return null;
            }

            if (propertyInfo.PropertyType == typeof(bool))
            {
                if (nodeProperty.Value.ToString() == "1") return true;

                return false;
            }

            if (propertyInfo.PropertyType == typeof(int))
            {
                int output;

                return int.TryParse(nodeProperty.Value.ToString(), out output) ? output : 0;
            }

            if (propertyInfo.PropertyType == typeof(decimal))
            {
                decimal output;

                return decimal.TryParse(nodeProperty.Value.ToString(), out output) ? output : 0;
            }

            var isCollection = typeof(IEnumerable<string>).IsAssignableFrom(propertyInfo.PropertyType);

            //ncrunch: no coverage start
            // Ignore the below code to PreValue being tightly coupled to the DB, bad code!
            if (isPreValue)
            {
                if (isCollection)
                {
                    var ids = nodeProperty.Value.ToString().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    var vals = ids.Select(x =>
                                              {
                                                  int number;
                                                  return int.TryParse(x, out number) ? new PreValue(number).Value : x;
                                              });

                    return vals.ToArray();
                }

                return new PreValue(int.Parse(nodeProperty.Value.ToString())).Value;
            }
            //ncrunch: no coverage end

            if (isCollection && nodeProperty.Value.ToString().Contains(","))
            {
                return nodeProperty.Value.ToString().Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            }

            if ( propertyInfo.PropertyType == typeof(DateTime))
            {
                return DateTime.Parse(nodeProperty.Value.ToString());
            }

            return nodeProperty.Value;
        }
    }
}