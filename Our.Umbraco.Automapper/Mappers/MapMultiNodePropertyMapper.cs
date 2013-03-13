using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Inflector;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Extensions;
using Umbraco.Core.Models;
using umbraco.NodeFactory;
using umbraco.interfaces;
using Media = umbraco.cms.businesslogic.media.Media;

namespace Our.Umbraco.Automapper.Mappers
{
    public class MapMultiNodePropertyMapper : AbstractPropertyMapper
    {
        private MapFromMultiNodeAttribute attr;

        protected override void MapCore<TDestination>(TDestination dest, IPublishedContent source, PropertyInfo propertyInfo)
        {
            attr = propertyInfo.GetAttribute<MapFromMultiNodeAttribute>();

            try
            {
                var camelized = propertyInfo.Name.Camelize();
                var propertyValue = source
                    .GetPropertyValue(camelized);

                if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString())) return;

                var nodes = propertyValue
                    .ToString().Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x =>
                                {
                                    var node = GetItem(x);
                                    return node;
                                })
                    .Where(x => x != null)
                    .ToList();

                if (!nodes.Any()) return;

                object mapTo;

                if (propertyInfo.PropertyType.CanBeCastTo<IEnumerable<object>>())
                    mapTo = Mapper.Map(nodes, SourceEnumerableType(), propertyInfo.PropertyType);
                else
                    mapTo = Mapper.Map(nodes.First(), SourceSingleType(), propertyInfo.PropertyType);

                propertyInfo.SetValue(dest, mapTo, null);
            }
            catch (Exception e)
            {
                throw new Exception("Error setting properties for node " + source.Id, e);
            }
        }

        private Type SourceSingleType()
        {
            if (attr.Type == NodeTypes.Media) return typeof(Media);

            return typeof(INode);
        }

        private Type SourceEnumerableType()
        {
            if (attr.Type == NodeTypes.Media) return typeof(IEnumerable<Media>);

            return typeof(IEnumerable<INode>);
        }

        private object GetItem(string x)
        {
            int id;
            if (int.TryParse(x, out id))
            {
                if (attr.Type == NodeTypes.Media) return new Media(id);

                return new Node(id);
            }

            return null;
        }

        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return propertyInfo.HasAttribute<MapFromMultiNodeAttribute>();
        }
    }
}