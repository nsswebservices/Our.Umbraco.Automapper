using System.Reflection;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Extensions;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    public class UmbracoPropertyAttributePropertyMapper : AbstractPropertyMapper
    {
        protected override void MapCore<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            var propAtt = propertyInfo.GetAttribute<UmbracoPropertyAttribute>();
            var propName = !string.IsNullOrEmpty(propAtt.PropertyAlias)
                               ? propAtt.PropertyAlias
                               : Inflector.Inflector.Camelize(propertyInfo.Name);

            object value = new ValueBuilder(source.GetProperty(propName), propertyInfo, propAtt.IsPreValue).GetValue();
            propertyInfo.SetValue(dest, value, null);
        }

        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return propertyInfo.HasAttribute<UmbracoPropertyAttribute>() && !propertyInfo.GetAttribute<UmbracoPropertyAttribute>().IsSpecialCase;
        }
    }
}