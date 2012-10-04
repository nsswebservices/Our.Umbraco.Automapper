using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper
{
    public class ValueBuilder
    {
        private readonly IProperty nodeProperty;
        private readonly PropertyInfo propertyInfo;
        private readonly bool isPreValue;

        public ValueBuilder(IProperty nodeProperty, PropertyInfo propertyInfo, bool isPreValue)
        {
            this.nodeProperty = nodeProperty;
            this.propertyInfo = propertyInfo;
            this.isPreValue = isPreValue;
        }

        public object GetValue()
        {
            if (nodeProperty == null || string.IsNullOrEmpty(nodeProperty.Value))
            {
                return null;
            }

            if (propertyInfo.PropertyType == typeof(bool))
            {
                if (nodeProperty.Value == "1") return true;

                return false;
            }

            if (propertyInfo.PropertyType == typeof(int))
            {
                int output;

                return int.TryParse(nodeProperty.Value, out output) ? output : 0;
            }

            if (propertyInfo.PropertyType == typeof(decimal))
            {
                decimal output;

                return decimal.TryParse(nodeProperty.Value, out output) ? output : 0;
            }

            var isCollection = typeof(IEnumerable<string>).IsAssignableFrom(propertyInfo.PropertyType);

            //ncrunch: no coverage start
            // Ignore the below code to PreValue being tightly coupled to the DB, bad code!
            if (isPreValue)
            {
                if (isCollection)
                {
                    var ids = nodeProperty.Value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    var vals = ids.Select(x =>
                                              {
                                                  int number;
                                                  return int.TryParse(x, out number) ? new PreValue(number).Value : x;
                                              });

                    return vals.ToArray();
                }

                return new PreValue(int.Parse(nodeProperty.Value)).Value;
            }
            //ncrunch: no coverage end

            if (isCollection && nodeProperty.Value.Contains(","))
            {
                return nodeProperty.Value.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            }

            if ( propertyInfo.PropertyType == typeof(DateTime))
            {
                return DateTime.Parse(nodeProperty.Value);
            }

            return nodeProperty.Value;
        }
    }
}