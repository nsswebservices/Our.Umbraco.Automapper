using AutoMapper;
using Our.Umbraco.Automapper.Mappers;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper
{
    public class DocumentAttributeConverter<TDestination> : TypeConverter<INode, TDestination> where TDestination : new()
    {
        private readonly IPropertyMapperPipeline<TDestination> mapperPipeline;

        public DocumentAttributeConverter(IPropertyMapperPipeline<TDestination> mapperPipeline)
        {
            this.mapperPipeline = mapperPipeline;
        }

        protected override TDestination ConvertCore(INode source)
        {
            var destination = new TDestination();

            mapperPipeline.Map(destination, source);

            return destination;
        }
    }
}