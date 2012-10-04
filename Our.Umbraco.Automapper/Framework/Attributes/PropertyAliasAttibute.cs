using System;

namespace Our.Umbraco.Automapper.Framework.Attributes
{
    public abstract class PropertyAliasAttibute : Attribute, IHasPropertyAliasAttribute
    {
        public string PropertyAlias { get; set; }
    }
}