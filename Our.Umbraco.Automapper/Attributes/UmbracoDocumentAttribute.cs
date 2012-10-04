using System;

namespace Our.Umbraco.Automapper.Attributes
{
    public class UmbracoDocumentAttribute : Attribute
    {
        public string DocumentAlias { get; set; }
    }
}