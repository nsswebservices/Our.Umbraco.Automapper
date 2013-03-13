using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Extensions;
using Umbraco.Core.Models;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    public delegate object ChildrenPropertyMapping(PropertyInfo propertyInfo, List<IPublishedContent> childrenAsList);

    public class ChildrenPropertyMapper : AbstractPropertyMapper
    {
        public ChildrenPropertyMapping MappingAction = (p, c) => Mapper.Map(c, typeof(List<IPublishedContent>), p.PropertyType);

        protected override void MapCore<TDestination>(TDestination dest, IPublishedContent source, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetAttribute<MapFromChildrenAttribute>();

            if (source.Children == null || !source.Children.Any()) return;

            var childrenAsList = source.Children.ToList();

            if (!string.IsNullOrEmpty(attr.NodeTypeAlias))
                childrenAsList = childrenAsList.Where(x => !string.IsNullOrEmpty(x.DocumentTypeAlias) 
                    && x.DocumentTypeAlias.Equals(attr.NodeTypeAlias, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();

            var mapped = MappingAction(propertyInfo, childrenAsList);

            propertyInfo.SetValue(dest, mapped, null);
        }

        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return propertyInfo.HasAttribute<MapFromChildrenAttribute>();
        }
    }
}