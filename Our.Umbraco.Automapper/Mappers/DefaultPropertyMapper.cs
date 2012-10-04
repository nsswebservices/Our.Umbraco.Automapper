using System.Reflection;
using Inflector;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    public class DefaultPropertyMapper : AbstractPropertyMapper
    {
        protected override void MapCore<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            var propName = propertyInfo.Name.Camelize();
            var prop = source.GetProperty(propName);

            if (prop == null) return;

            var value = new ValueBuilder(source.GetProperty(propName), propertyInfo, false).GetValue();

            propertyInfo.SetValue(dest, value, null);
        }

        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return true;
        }
    }
}