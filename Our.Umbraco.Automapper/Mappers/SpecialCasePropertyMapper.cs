using System;
using System.Collections.Generic;
using System.Reflection;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Extensions;
using Umbraco.Core.Models;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    public class SpecialCasePropertyMapper : AbstractPropertyMapper
    {
        private static readonly IDictionary<string, Action<dynamic, IPublishedContent, PropertyInfo>> specialCases =
            new Dictionary<string, Action<dynamic, IPublishedContent, PropertyInfo>>(StringComparer.InvariantCultureIgnoreCase)
                {
                    {"name", (x, y, z) => z.SetValue(x, y.Name, null)},
                    {"url", (x, y, z) => z.SetValue(x, y.Url, null)},
                    {"id", (x, y, z) => z.SetValue(x, y.Id, null)},
                    {
                        "parentname", (x, y, z) =>
                                          {
                                              if (y.Parent != null)
                                                  z.SetValue(x, y.Parent.Name, null);
                                          }
                    },
                    {"nodetypealias", (x, y, z) => z.SetValue(x, y.DocumentTypeAlias, null)},
                    {"createdate", (x, y, z) => z.SetValue(x, y.CreateDate, null)},
                    {"level", (x, y, z) => z.SetValue(x, y.Level, null)}
                };

        protected override void MapCore<TDestination>(TDestination dest, IPublishedContent source, PropertyInfo propertyInfo)
        {
            var propAtt = propertyInfo.GetAttribute<UmbracoPropertyAttribute>();

            if (propAtt == null)
            {
                specialCases[propertyInfo.Name](dest, source, propertyInfo);
                return;
            }

            specialCases[propAtt.PropertyAlias](dest, source, propertyInfo);
        }

        protected override bool CanPerformActionCore(PropertyInfo propertyInfo)
        {
            var propAtt = propertyInfo.GetAttribute<UmbracoPropertyAttribute>();

            if (propAtt == null)
                return specialCases.ContainsKey(propertyInfo.Name);

            return !string.IsNullOrEmpty(propAtt.PropertyAlias) && specialCases.ContainsKey(propAtt.PropertyAlias);
        }
    }
}