using System;
using System.Reflection;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    internal class EmptyPropertyMapper : IPropertyMapper
    {
        public IPropertyMapper Next { get; private set; }

        public static IPropertyMapper Instance = new EmptyPropertyMapper();

        public void Map<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            throw new InvalidOperationException("An EmptyPropertyMapper can not map a property");
        }

        public bool CanPerformAction(PropertyInfo propertyInfo)
        {
            return false;
        }

        public IPropertyMapper SetNext(IPropertyMapper n)
        {
            throw new InvalidOperationException("Can not set a next item on EmptyPropertyMapper");
        }
    }
}