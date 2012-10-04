using System;
using System.Reflection;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Extensions;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    public abstract class AbstractHasPropertyAliasPropertyMapper<TAttribute, TProperty, TItem> : AbstractPropertyMapper
        where TAttribute : Attribute, IHasPropertyAliasAttribute
    {
        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            return propertyInfo.HasAttribute<TAttribute>() ||
                   propertyInfo.PropertyType.CanBeCastTo<TProperty>();
        }

        protected override void MapCore<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            var att = propertyInfo.GetAttribute<TAttribute>();

            var alias = GetAlias(propertyInfo, att);

            var raw = source.GetPropertyValueOrEmpty(alias);

            if (string.IsNullOrWhiteSpace(raw))
                return;

            var urls = ConstructItem(raw);

            MapPropertyCore(dest, propertyInfo, urls);
        }

        private static string GetAlias(PropertyInfo propertyInfo, TAttribute att)
        {
            return ShouldGetAliasFromPropertyInfo(att) ? Inflector.Inflector.Camelize(propertyInfo.Name) : att.PropertyAlias;
        }

        private static bool ShouldGetAliasFromPropertyInfo(TAttribute att)
        {
            return att == null || string.IsNullOrWhiteSpace(att.PropertyAlias);
        }

        protected abstract TItem ConstructItem(string raw);

        protected virtual void MapPropertyCore<TDestination>(TDestination dest, PropertyInfo propertyInfo, TItem urls)
        {
            propertyInfo.SetValue(dest, urls, null);
        }
    }
}