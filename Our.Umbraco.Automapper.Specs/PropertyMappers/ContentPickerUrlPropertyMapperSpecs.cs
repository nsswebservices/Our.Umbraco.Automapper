using Machine.Specifications;
using Our.Umbraco.Automapper.Framework.Mappers;

namespace Our.Umbraco.Automapper.Specs.PropertyMappers
{
    [Subject(typeof(ContentPickerUrlPropertyMapper))]
    public class when_mapping_a_content_url_and_there_is_a_value
    {
        private static ContentPickerUrlPropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;


        private Establish context = () =>
                                        {
                                            mapper = new ContentPickerUrlPropertyMapper{CreateNode = (i) => new MapperTestNode {Url = "/expected"}};
                                            testHelper = new PropertyMapperTestHelper(mapper, x => x.AddProperty("contentPickerUrl", 1234), "ContentPickerUrl");
                                        };

        private Because of = () => testHelper.Execute();

        private It should_map_the_property = () => testHelper.Result.ContentPickerUrl.ShouldNotBeNull();
    }

    [Subject(typeof(ContentPickerUrlPropertyMapper))]
    public class when_mapping_a_content_url_and_there_is_no_value
    {
        private static ContentPickerUrlPropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;


        private Establish context = () =>
        {
            mapper = new ContentPickerUrlPropertyMapper { CreateNode = (i) => new MapperTestNode() };
            testHelper = new PropertyMapperTestHelper(mapper, x => x.AddProperty("contentPickerUrl", null), "ContentPickerUrl");
        };

        private Because of = () => testHelper.Execute();

        private It should_map_the_property = () => testHelper.Result.ContentPickerUrl.ShouldBeNull();
    }

    [Subject(typeof(ContentPickerUrlPropertyMapper))]
    public class when_mapping_a_content_url_and_is_not_an_int
    {
        private static ContentPickerUrlPropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;


        private Establish context = () =>
        {
            mapper = new ContentPickerUrlPropertyMapper { CreateNode = (i) => new MapperTestNode() };
            testHelper = new PropertyMapperTestHelper(mapper, x => x.AddProperty("contentPickerUrl", "sadasd"), "ContentPickerUrl");
        };

        private Because of = () => testHelper.Execute();

        private It should_map_the_property = () => testHelper.Result.ContentPickerUrl.ShouldBeNull();
    }
}