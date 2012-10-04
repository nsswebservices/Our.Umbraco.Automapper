using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Machine.Specifications;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Specs
{
    [Subject(typeof(ValueBuilder))]
    public class when_mapping_an_int
    {
        private static ValueBuilder builder;
        private static object value;

        private Establish context = () => builder = ValueBuilderHelper.Build("Int", Tuple.Create("int", 1));

        private Because of = () => value = builder.GetValue();

        private It should_an_int = () => value.ShouldBeOfType<int>();

        private It should_an_correct_value = () => value.ShouldEqual(1);
    }


    [Subject(typeof(ValueBuilder))]
    public class when_mapping_a_bool_that_is_true
    {
        private static ValueBuilder builder;
        private static object value;

        private Establish context = () => builder = ValueBuilderHelper.Build("Bool", Tuple.Create("bool", 1));

        private Because of = () => value = builder.GetValue();

        private It should_a_bool = () => value.ShouldBeOfType<bool>();

        private It should_an_correct_value = () => value.ShouldEqual(true);
    }


    [Subject(typeof(ValueBuilder))]
    public class when_mapping_a_bool_that_is_false
    {
        private static ValueBuilder builder;
        private static object value;

        private Establish context = () => builder = ValueBuilderHelper.Build("Bool", Tuple.Create("bool", 0));

        private Because of = () => value = builder.GetValue();

        private It should_a_bool = () => value.ShouldBeOfType<bool>();

        private It should_an_correct_value = () => value.ShouldEqual(false);
    }


    [Subject(typeof(ValueBuilder))]
    public class when_mapping_a_decimal
    {
        private static ValueBuilder builder;
        private static object value;
        private static double expected = 1d;

        private Establish context = () => builder = ValueBuilderHelper.Build("Dec", Tuple.Create("dec", expected));

        private Because of = () => value = builder.GetValue();

        private It should_a_decimal = () => value.ShouldBeOfType<decimal>();

        private It should_an_correct_value = () => value.ShouldEqual((decimal)expected);
    }

    [Subject(typeof(ValueBuilder))]
    public class when_mapping_a_property_that_is_null
    {
        private static ValueBuilder builder;
        private static object value;
        private static double expected = 1d;

        private Establish context = () => builder = ValueBuilderHelper.Build("Null", Tuple.Create("null", (string)null));

        private Because of = () => value = builder.GetValue();

        private It should_a_correct_value = () => value.ShouldBeNull();
    }

    [Subject(typeof(ValueBuilder))]
    public class when_mapping_a_date
    {
        private static ValueBuilder builder;
        private static object value;
        private static DateTime expected = new DateTime(2012, 4, 17);

        private Establish context = () => builder = ValueBuilderHelper.Build("Date", Tuple.Create("date", expected));

        private Because of = () => value = builder.GetValue();

        private It should_a_date = () => value.ShouldBeOfType<DateTime>();

        private It should_an_correct_value = () => value.ShouldEqual(expected);
    }

    [Subject(typeof(ValueBuilder))]
    public class when_mapping_a_collection_of_primative_types
    {
        private static ValueBuilder builder;
        private static object value;
        private static string expected = "how,now,brown,omg,zombies";

        private Establish context = () => builder = ValueBuilderHelper.Build("Strings", Tuple.Create("strings", expected));

        private Because of = () => value = builder.GetValue();

        private It should_a_collection_of_strings = () => value.ShouldBeOfType<IEnumerable<string>>();

        private It should_an_correct_value = () => ((IEnumerable<string>)value).First().ShouldEqual("how");
    }

    public  class ValueBuilderTarget
    {
        public int Int { get; set; }
        public bool Bool { get; set; }
        public decimal Dec { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<string> Strings { get; set; }
        public string Null { get; set; }
    }

    internal class ValueBuilderHelper
    {
        public static ValueBuilder Build<T>(string propName, Tuple<string, T> propValue)
        {
            return new ValueBuilder(GetUmbracoProperty(propValue), GetPropertyInfo(propName), false);
        }

        private static PropertyInfo GetPropertyInfo(string propName)
        {
            return typeof(ValueBuilderTarget).GetProperty(propName);
        }

        private static IProperty GetUmbracoProperty<T>(Tuple<string, T> propValue)
        {
            return new TestProperty(propValue.Item1, propValue.Item2);
        }
    }
}