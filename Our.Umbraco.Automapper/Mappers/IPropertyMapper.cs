using System.Reflection;
using Umbraco.Core.Models;

namespace Our.Umbraco.Automapper.Mappers
{
    public interface IPropertyMapper
    {
        void Map<TDestination>(TDestination dest, IPublishedContent source, PropertyInfo propertyInfo);
        bool CanPerformAction(PropertyInfo propertyInfo);
    }
}