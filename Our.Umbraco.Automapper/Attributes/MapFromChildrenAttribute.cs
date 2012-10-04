using System;

namespace Our.Umbraco.Automapper.Attributes
{
    public class MapFromChildrenAttribute : Attribute
    {
        public string NodeTypeAlias { get; set; }
    }
}