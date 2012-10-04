using System.Reflection;
using Inflector;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Extensions;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    public class ContentPickerUrlPropertyMapper : AbstractPropertyMapper
    {
        protected override void MapCore<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetAttribute<MapFromContentPickerUrlAttribute>();
            var propName = string.IsNullOrWhiteSpace(attr.PropertyAlias) ? propertyInfo.Name.Camelize() : attr.PropertyAlias;

            var propValueRaw = source.GetPropertyValue(propName);

            if (propValueRaw == null || string.IsNullOrWhiteSpace(propValueRaw.ToString()))
                return;

            var rawId = propValueRaw.ToString();

            int mediaId;
            if (!int.TryParse(rawId, out mediaId))
                return;

            var node = NodeBuilder(mediaId);

            var fileName = node.Url;
            propertyInfo.SetValue(dest, fileName, null);
        }

        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return propertyInfo.HasAttribute<MapFromContentPickerUrlAttribute>();
        }
    }
}