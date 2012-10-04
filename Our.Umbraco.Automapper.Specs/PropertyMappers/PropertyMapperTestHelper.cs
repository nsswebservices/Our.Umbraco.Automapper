using System;
using Our.Umbraco.Automapper.Mappers;

namespace Our.Umbraco.Automapper.Specs.PropertyMappers
{
    internal class PropertyMapperTestHelper
    {
        private readonly IPropertyMapper mapper;
        private readonly Action<MapperTestNode> prepareNode;
        private readonly string propertyName;
        private MapperTestItem result;

        public MapperTestItem Result
        {
            get { return result; }
        }

        public PropertyMapperTestHelper(IPropertyMapper mapper, Action<MapperTestNode> prepareNode, string propertyName)
        {
            this.mapper = mapper;
            this.prepareNode = prepareNode;
            this.propertyName = propertyName;
            result = new MapperTestItem();
        }


        public void Execute()
        {
            var mapperTestNode = new MapperTestNode();
            prepareNode(mapperTestNode);
            mapper.Map(result, mapperTestNode, typeof (MapperTestItem).GetProperty(propertyName));
        }
    }
}