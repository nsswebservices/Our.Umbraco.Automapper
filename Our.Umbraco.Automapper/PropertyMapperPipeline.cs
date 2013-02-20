using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Our.Umbraco.Automapper.Mappers;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper
{
    public class PropertyMapperPipeline<T> : IPropertyMapperPipeline<T>
    {
        public static readonly Func<PropertyMapperPipeline<T>> Default = () => new PropertyMapperPipeline<T>(Pipelines.Default);

        private readonly IDictionary<PropertyInfo, IPropertyMapper> mapperCache = new Dictionary<PropertyInfo, IPropertyMapper>();

        public IDictionary<PropertyInfo, IPropertyMapper> MapperCache
        {
            get { return mapperCache; }
        }

        public PropertyMapperPipeline(IEnumerable<IPropertyMapper> all)
        {
            var properties = typeof(T).GetProperties();
            var mappers = all.ToList();

            foreach (var propertyInfo in properties)
            {
                var localProp = propertyInfo;
                var items = mappers.First(x => x.CanPerformAction(localProp));
                mapperCache.Add(propertyInfo, items);
            }
        }

        public void Map(T destination, INode source)
        {
            foreach (var propertyMapper in MapperCache)
            {
                propertyMapper.Value.Map(destination, source, propertyMapper.Key);
            }
        }
    }
}