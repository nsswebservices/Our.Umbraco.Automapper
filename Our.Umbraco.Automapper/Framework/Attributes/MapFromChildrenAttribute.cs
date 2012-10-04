using System;

namespace Our.Umbraco.Automapper.Framework.Attributes
{
    public class MapFromChildrenAttribute : Attribute
    {
        public string NodeTypeAlias { get; set; }
    }
}