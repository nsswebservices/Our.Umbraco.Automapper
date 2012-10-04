using Machine.Specifications;
using Our.Umbraco.Automapper.Framework.Mappers;

namespace Our.Umbraco.Automapper.Specs.PropertyMappers
{
    [Subject(typeof(UmbracoPropertyAttributePropertyMapper))]
    public class when_mapping_using_an_UmbracoPropertyAttribute
    {
        private static UmbracoPropertyAttributePropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;


        private Establish context = () =>
        {
            mapper = new UmbracoPropertyAttributePropertyMapper { CreateNode = (i) => new MapperTestNode() };
            testHelper = new PropertyMapperTestHelper(mapper, x => x.AddProperty("propertyAttribute", "expected value"), "PropertyAttribute");
        };

        private Because of = () => testHelper.Execute();

        private It should_map_the_property = () => testHelper.Result.PropertyAttribute.ShouldEqual("expected value");
    }

    [Subject(typeof(UmbracoPropertyAttributePropertyMapper))]
    public class when_mapping_using_an_UmbracoPropertyAttribute_with_an_alias
    {
        private static UmbracoPropertyAttributePropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;


        private Establish context = () =>
        {
            mapper = new UmbracoPropertyAttributePropertyMapper { CreateNode = (i) => new MapperTestNode() };
            testHelper = new PropertyMapperTestHelper(mapper, x => x.AddProperty("propertyAttributeAlias", "expected value"), "PropertyAttributeWithAnAlias");
        };

        private Because of = () => testHelper.Execute();

        private It should_map_the_property = () => testHelper.Result.PropertyAttributeWithAnAlias.ShouldEqual("expected value");
    }
}