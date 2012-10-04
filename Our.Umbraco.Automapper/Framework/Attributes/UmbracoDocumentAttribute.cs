using System;

namespace Our.Umbraco.Automapper.Framework.Attributes
{
    public class UmbracoDocumentAttribute : Attribute
    {
        public string DocumentAlias { get; set; }
    }
}