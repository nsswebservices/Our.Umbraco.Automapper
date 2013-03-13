using System;

namespace Our.Umbraco.Automapper.Attributes
{
    public class MapFromChildrenAttribute : Attribute
    {
        public string DocumentTypeAlias { get; set; }
    }
}