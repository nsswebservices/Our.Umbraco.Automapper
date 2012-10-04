using System.Reflection;
using Our.Umbraco.Automapper.Extensions;
using Our.Umbraco.Automapper.Framework.Attributes;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Framework.Mappers
{
    public class ShouldIgnorePropertyMapper : AbstractPropertyMapper
    {
        protected override void MapCore<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            // dothing nothing
        }

        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return propertyInfo.HasAttribute<UmbracoIgnorePropertyAttribute>();
        }
    }
}