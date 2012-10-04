using System.Linq;
using Machine.Specifications;
using Our.Umbraco.Automapper.Framework.Mappers;

namespace Our.Umbraco.Automapper.Specs.PropertyMappers
{
    [Subject(typeof(ChildrenPropertyMapper))]
    public class when_mapping_a_nodes_childrens
    {
        private static ChildrenPropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;

        private Establish context = () =>
                                        {
                                            mapper = new ChildrenPropertyMapper();
                                            mapper.MappingAction = (info, list) => Enumerable.Range(0, list.Count).Select(x => new MapperTestItem());
                                            testHelper = new PropertyMapperTestHelper(mapper,
                                                                                      x =>
                                                                                      x.ChildrenAsList.AddRange(new[]
                                                                                    {
                                                                                        new MapperTestNode(),
                                                                                        new MapperTestNode() {NodeTypeAlias = "Filtered"},
                                                                                    }), "SomeKids");
                                        };

        private Because of = () => testHelper.Execute();

        private It should_map_the_property = () => testHelper.Result.SomeKids.Count().ShouldEqual(2);
    }

    [Subject(typeof(ChildrenPropertyMapper))]
    public class when_mapping_a_nodes_childrens_and_want_a_specific_type
    {
        private static ChildrenPropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;

        private Establish context = () =>
        {
            mapper = new ChildrenPropertyMapper();
            mapper.MappingAction = (info, list) => Enumerable.Range(0, list.Count).Select(x => new MapperTestItem());
            testHelper = new PropertyMapperTestHelper(mapper,
                                                      x =>
                                                      x.ChildrenAsList.AddRange(new[]
                                                                                    {
                                                                                        new MapperTestNode(),
                                                                                        new MapperTestNode() {NodeTypeAlias = "Filtered"},
                                                                                    }), "FilteredKids");
        };

        private Because of = () => testHelper.Execute();

        private It should_map_the_property = () => testHelper.Result.FilteredKids.Count().ShouldEqual(1);
    }
}