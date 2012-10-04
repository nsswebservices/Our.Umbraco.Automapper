using System;

namespace Our.Umbraco.Automapper.Attributes
{
    public abstract class PropertyAliasAttibute : Attribute, IHasPropertyAliasAttribute
    {
        public string PropertyAlias { get; set; }
    }
}