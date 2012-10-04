using System.Reflection;
using Inflector;
using Our.Umbraco.Automapper.Extensions;
using Our.Umbraco.Automapper.Framework.Attributes;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Framework.Mappers
{
    public class MediaPickerPropertyMapper : AbstractPropertyMapper
    {
        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return propertyInfo.HasAttribute<MapFromMediaPickerAttribute>();
        }

        protected override void MapCore<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetAttribute<MapFromMediaPickerAttribute>();

            var propName = string.IsNullOrWhiteSpace(attr.PropertyAlias) ? propertyInfo.Name.Camelize() : attr.PropertyAlias;
            var propValueRaw = source.GetPropertyValue(propName);

            if (propValueRaw == null || string.IsNullOrWhiteSpace(propValueRaw.ToString()))
                return;

            var rawId = propValueRaw.ToString();

            int mediaId;
            if (!int.TryParse(rawId, out mediaId))
                return;

            var media = new Media(mediaId);

            var fileName = media.GetPropertyValue("umbracoFile").ToString();
            propertyInfo.SetValue(dest, fileName, null);
        }
    }
}