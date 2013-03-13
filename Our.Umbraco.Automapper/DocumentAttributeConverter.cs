using AutoMapper;
using Our.Umbraco.Automapper.Mappers;
using Umbraco.Core.Models;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper
{
    public class DocumentAttributeConverter<TDestination> : TypeConverter<IPublishedContent, TDestination> where TDestination : new()
    {
        private readonly IPropertyMapperPipeline<TDestination> mapperPipeline;

        public DocumentAttributeConverter(IPropertyMapperPipeline<TDestination> mapperPipeline)
        {
            this.mapperPipeline = mapperPipeline;
        }

        protected override TDestination ConvertCore(IPublishedContent source)
        {
            var destination = new TDestination();

            mapperPipeline.Map(destination, source);

            return destination;
        }
    }
}