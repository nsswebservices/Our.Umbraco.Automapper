using System;
using System.Linq;
using Machine.Specifications;
using Our.Umbraco.Automapper.Framework.Mappers;

namespace Our.Umbraco.Automapper.Specs.PropertyMappers
{
    [Subject(typeof(EmptyPropertyMapper))]
    public class when_asked_if_mapper_can_map
    {
        private static EmptyPropertyMapper mapper;
        private static bool result;

        private Establish context = () =>
                                        {
                                            mapper = new EmptyPropertyMapper();
                                        };

        private Because of = () => result = mapper.CanPerformAction(typeof (MapperTestItem).GetProperties().First());

        private It should_not_be_able_to = () => result.ShouldBeFalse();
    }

    [Subject(typeof(EmptyPropertyMapper))]
    public class when_mapping_an_item
    {
        private static EmptyPropertyMapper mapper;
        private static Exception exception;

        private Establish context = () =>
                                        {
                                            mapper = new EmptyPropertyMapper();
                                        };

        private Because of = () => exception = Catch.Exception(() => mapper.Map(new MapperTestItem(), new MapperTestNode(), typeof(MapperTestItem).GetProperties().First()));

        private It should_throw_an_exception = () => exception.ShouldNotBeNull();

        private It should_not_be_able_to = () => exception.ShouldBeOfType<InvalidOperationException>();
    }


    [Subject(typeof(EmptyPropertyMapper))]
    public class when_setting_next
    {
        private static EmptyPropertyMapper mapper;
        private static Exception exception;

        private Establish context = () =>
        {
            mapper = new EmptyPropertyMapper();
        };

        private Because of = () => exception = Catch.Exception(() => mapper.SetNext(new EmptyPropertyMapper()));

        private It should_throw_an_exception = () => exception.ShouldNotBeNull();

        private It should_not_be_able_to = () => exception.ShouldBeOfType<InvalidOperationException>();
    }
}