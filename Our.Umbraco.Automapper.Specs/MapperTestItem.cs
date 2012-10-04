using System.Collections.Generic;
using Our.Umbraco.Automapper.Framework.Attributes;

namespace Our.Umbraco.Automapper.Specs
{
    public class MapperTestItem
    {
        [MapFromContentPickerUrl]
        public string ContentPickerUrl { get; set; }

        [MapFromChildren]
        public IEnumerable<MapperTestItem> SomeKids { get; set; }

        [MapFromChildren(NodeTypeAlias = "Filtered")]
        public IEnumerable<MapperTestItem> FilteredKids { get; set; }

        [UmbracoProperty]
        public string PropertyAttribute { get; set; }

        [UmbracoProperty(PropertyAlias = "propertyAttributeAlias")]
        public string PropertyAttributeWithAnAlias { get; set; }
    }
}