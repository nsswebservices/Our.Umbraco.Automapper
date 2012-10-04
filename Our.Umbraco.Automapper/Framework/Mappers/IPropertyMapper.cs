using System.Reflection;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Framework.Mappers
{
    public interface IPropertyMapper
    {
        IPropertyMapper Next { get; }

        void Map<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo);
        bool CanPerformAction(PropertyInfo propertyInfo);
        IPropertyMapper SetNext(IPropertyMapper n);
    }
}