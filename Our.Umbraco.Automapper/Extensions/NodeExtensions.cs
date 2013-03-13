using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Umbraco.Core.Models;
using umbraco.interfaces;
using Media = umbraco.cms.businesslogic.media.Media;

namespace Our.Umbraco.Automapper.Extensions
{
    public static class NodeExtensions
    {
        public static object GetPropertyValue(this INode node, string alias)
        {
            var prop = node.GetProperty(alias);

            return prop == null ? null : prop.Value;
        }

        public static string GetPropertyValueOrEmpty(this INode node, string alias)
        {
            var prop = node.GetPropertyValue(alias);

            return prop == null ? string.Empty : prop.ToString();
        }

        public static object GetPropertyValue(this IPublishedContent node, string alias)
        {
            var prop = node.GetProperty(alias);

            return prop == null ? null : prop.Value;
        }

        public static string GetPropertyValueOrEmpty(this IPublishedContent node, string alias)
        {
            var prop = node.GetPropertyValue(alias);

            return prop == null ? string.Empty : prop.ToString();
        }

        public static object GetPropertyValue(this Media node, string alias)
        {
            var prop = node.getProperty(alias);

            return prop == null ? null : prop.Value;
        }

        public static T FromXml<T>(this object obj)
        {
            var serializer = new XmlSerializer(typeof(T));

            var reader = new StringReader(obj.ToString());
            var r = (T)serializer.Deserialize(reader);
            reader.Close();

            return r;
        }

        public static IEnumerable<int> GetPathAsList(this INode node)
        {
            return node.Path.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).Except(new[] {-1});
        }
    }
}