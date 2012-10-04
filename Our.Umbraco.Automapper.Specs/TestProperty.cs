using System;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Specs
{
    public class TestProperty : IProperty
    {
        private string value;

        public TestProperty(string @alias, object propsValue)
        {
            Alias = alias;
            PropsValue = propsValue;

        }

        public string Alias { get; private set; }

        public object PropsValue { get; set; }

        public string Value
        {
            get { return (PropsValue ?? "").ToString(); }
        }

        public Guid Version { get; private set; }
    }
}