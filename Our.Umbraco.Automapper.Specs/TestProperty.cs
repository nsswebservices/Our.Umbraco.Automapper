using System;
using Umbraco.Core.Models;

namespace Our.Umbraco.Automapper.Specs
{
    public class TestProperty : IPublishedContentProperty
    {
        private string value;

        public TestProperty(string @alias, object propsValue)
        {
            Alias = alias;
            PropsValue = propsValue;
        }

        public string Alias { get; private set; }

        public object PropsValue { get; set; }

        public object Value
        {
            get { return (PropsValue ?? ""); }
        }

        public Guid Version { get; private set; }
    }
}