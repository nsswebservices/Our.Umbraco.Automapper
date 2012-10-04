namespace Our.Umbraco.Automapper.Framework.Attributes
{
    public class UmbracoPropertyAttribute : PropertyAliasAttibute
    {
        public bool IsPreValue { get; set; }

        public bool IsSpecialCase { get; set; }
    }
}