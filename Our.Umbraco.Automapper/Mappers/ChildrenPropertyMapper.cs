using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Extensions;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    public delegate object ChildrenPropertyMapping(PropertyInfo propertyInfo, List<INode> childrenAsList);

    public class ChildrenPropertyMapper : AbstractPropertyMapper
    {
        public ChildrenPropertyMapping MappingAction = (p, c) => Mapper.Map(c, typeof (List<INode>), p.PropertyType);

        protected override void MapCore<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetAttribute<MapFromChildrenAttribute>();
            var childrenAsList = source.ChildrenAsList;

            if (!string.IsNullOrEmpty(attr.NodeTypeAlias))
                childrenAsList = childrenAsList.Where(x => !string.IsNullOrEmpty(x.NodeTypeAlias) 
                    && x.NodeTypeAlias.Equals(attr.NodeTypeAlias, StringComparison.InvariantCultureIgnoreCase))
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