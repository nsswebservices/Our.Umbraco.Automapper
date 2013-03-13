using System;
using System.Reflection;
using Umbraco.Core.Models;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    internal class EmptyPropertyMapper : IPropertyMapper
    {
        public static IPropertyMapper Instance = new EmptyPropertyMapper();

        public void Map<TDestination>(TDestination dest, IPublishedContent source, PropertyInfo propertyInfo)
        {
            throw new InvalidOperationException("An EmptyPropertyMapper can not map a property");
        }

        public bool CanPerformAction(PropertyInfo propertyInfo)
        {
            return false;
        }
    }
}