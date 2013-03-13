using System.Reflection;
using Inflector;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Extensions;
using Umbraco.Core.Models;
using umbraco.interfaces;
using Media = umbraco.cms.businesslogic.media.Media;

namespace Our.Umbraco.Automapper.Mappers
{
    public class MediaPickerPropertyMapper : AbstractPropertyMapper
    {
        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return propertyInfo.HasAttribute<MapFromMediaPickerAttribute>();
        }

        protected override void MapCore<TDestination>(TDestination dest, IPublishedContent source, PropertyInfo propertyInfo)
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