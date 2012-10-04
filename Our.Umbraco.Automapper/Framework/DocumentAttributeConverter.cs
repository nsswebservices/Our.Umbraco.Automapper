using AutoMapper;
using Our.Umbraco.Automapper.Framework.Mappers;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Framework
{
    public class DocumentAttributeConverter<TDestination> : TypeConverter<INode, TDestination> where TDestination : new()
    {
        private readonly IPropertyMapper propertyMapper;

        public DocumentAttributeConverter(IPropertyMapper propertyMapper)
        {
            this.propertyMapper = propertyMapper;
        }

        protected override TDestination ConvertCore(INode source)
        {
            var destination = new TDestination();

            MapPropertiesFromAttributes(destination, source);

            return destination;
        }

        protected void MapPropertiesFromAttributes(TDestination dest, INode source)
        {
            var properties = typeof (TDestination).GetProperties();

            foreach (var propertyInfo in properties)
            {
                propertyMapper.Map(dest, source, propertyInfo);
            }
        }
    }
}