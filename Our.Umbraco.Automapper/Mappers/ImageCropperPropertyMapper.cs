using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Extensions;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    public class ImageCropperPropertyMapper : AbstractPropertyMapper
    {
        protected override void MapCore<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            var propAtt = propertyInfo.GetAttribute<MapAsResizedImageAttribute>();

            var rawXml = source.GetPropertyValue(propAtt.PropertyAlias);
            if (rawXml == null || string.IsNullOrEmpty(rawXml.ToString())) return;

            var crops = rawXml.FromXml<Crops>();

            if (crops != null && crops.Count > 0)
            {
                propertyInfo.SetValue(dest, crops.First().Url, null);
            }
        }

        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return propertyInfo.HasAttribute<MapAsResizedImageAttribute>();
        }
    }

    [XmlType(TypeName = "crops")]
    public class Crops : List<Crop>
    {
        [XmlAttribute(AttributeName = "date", DataType = "date")]
        public DateTime Date { get; set; }
    }

    [XmlType(TypeName = "crop")]
    public class Crop
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }
    }
}