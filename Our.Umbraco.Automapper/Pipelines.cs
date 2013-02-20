using System.Collections.Generic;
using Our.Umbraco.Automapper.Mappers;

namespace Our.Umbraco.Automapper
{
    public static class Pipelines
    {
        public static readonly IEnumerable<IPropertyMapper> Default = new List<IPropertyMapper>
            {
                new ShouldIgnorePropertyMapper(),
                new UmbracoPropertyAttributePropertyMapper(),
                new MapMultiUrlPickerPropertyMapper(),
                new MapUrlPickerPropertyMapper(),
                new MapMultiNodePropertyMapper(),
                new ContentPickerUrlPropertyMapper(),
                new MediaPickerPropertyMapper(),
                new ChildrenPropertyMapper(),
                new SpecialCasePropertyMapper(),
                new DefaultPropertyMapper()
            };
    }
}