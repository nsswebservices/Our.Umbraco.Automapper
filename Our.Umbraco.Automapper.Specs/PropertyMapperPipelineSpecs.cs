using System.Linq;
using Machine.Specifications;
using Our.Umbraco.Automapper.Specs.PropertyMappers;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Specs
{
    [Subject(typeof(PropertyMapperPipeline<MapperTestItem>))]
    public class should_construct_the_pipeline
    {
        private static PropertyMapperPipeline<MapperTestItem> pipeline;

        private Establish context = () => { };

        Because of = () => pipeline = new PropertyMapperPipeline<MapperTestItem>(Pipelines.Default);

        private It should_build_the_correct_pipeline = () => pipeline.MapperCache.Count().ShouldEqual(5);
    }

    [Subject(typeof(PropertyMapperPipeline<MapperTestItem>))]
    public class should_map
    {
        private static PropertyMapperPipeline<MapperTestItem> pipeline;
        private static MapperTestItem result;
        private static PropertyMapperTestHelper testHelper;
        private static MapperTestNode source;

        private Establish context = () =>
            {
                result = new MapperTestItem();
                source = new MapperTestNode();
                source.AddProperty("propertyAttribute", "expected");
                pipeline = PropertyMapperPipeline<MapperTestItem>.Default();
            };

        private Because of = () => pipeline.Map(result, source);

        private It should_build_the_correct_pipeline = () => result.PropertyAttribute.ShouldEqual("expected");
    }
}