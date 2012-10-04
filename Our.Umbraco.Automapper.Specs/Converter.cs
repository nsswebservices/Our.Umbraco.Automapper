using System;
using System.Collections.Generic;
using System.Data;
using AutoMapper;
using Machine.Specifications;
using Storm.Umbraco.ObjectMapping.Domain;
using Storm.Umbraco.ObjectMapping.Framework;
using Storm.Umbraco.ObjectMapping.Framework.Mappers;
using umbraco.interfaces;

namespace Storm.Umbrac.ObjectMapping.Tests
{
    [Subject(typeof(DocumentAttributeConverter<>))]
    public class when_convering_an_inode
    {
        private static TestItem result;

        private Establish context = () => Mapper.CreateMap<INode, TestItem>()
                                              .ConvertUsing(new DocumentAttributeConverter<TestItem>(ObjectMappingSettings.Default));

        private Because of = () =>
                                 {
                                     result = Mapper.Map<INode, TestItem>(new TestNode()
                                                                              {
                                                                                  Id = 1,
                                                                                  Url = "/url",
                                                                                  Name = "test"
                                                                              });
                                 };

        private It should_set_name = () =>
                                        {
                                            result.Name.ShouldNotBeNull();
                                        };
    }

    public class TestNode : INode
    {
        private IDictionary<string, object> propsValues = new Dictionary<string, object>()
                                                              {
                                                                 { "umbracoNaviHide", false},
                                                                 { "heading", "test "},
                                                                 { "metaTitle", "test "},
                                                                 { "metaDescription", "test "},
                                                                 { "metaKeywords", "test "},
                                                              };

        public IProperty GetProperty(string Alias)
        {
            Console.WriteLine(Alias);
            return new TestPropert(Alias, propsValues[Alias]);
        }

        public IProperty GetProperty(string Alias, out bool propertyExists)
        {
            throw new NotImplementedException();
        }

        public DataTable ChildrenAsTable()
        {
            throw new NotImplementedException();
        }

        public DataTable ChildrenAsTable(string nodeTypeAliasFilter)
        {
            throw new NotImplementedException();
        }

        public INode Parent { get; private set; }
        public int Id { get; set; }
        public int template { get; private set; }
        public int SortOrder { get; private set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string UrlName { get; private set; }
        public string NodeTypeAlias { get; private set; }
        public string WriterName { get; private set; }
        public string CreatorName { get; private set; }
        public int WriterID { get; private set; }
        public int CreatorID { get; private set; }
        public string Path { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public Guid Version { get; private set; }
        public string NiceUrl { get; private set; }
        public int Level { get; private set; }
        public List<IProperty> PropertiesAsList { get; private set; }
        public List<INode> ChildrenAsList { get; private set; }
    }

    internal class TestItem : Content
    {
    }

    internal class ObjectMappingSettings
    {
        private static readonly IPropertyMapper defaultMap;

        static ObjectMappingSettings()
        {
            var pipe = new ShouldIgnorePropertyMapper();

            pipe.SetNext(new AttributePropertyMapper())
                .SetNext(new MapMultiUrlPickerPropertyMapper())
                .SetNext(new MapUrlPickerPropertyMapper())
                .SetNext(new MapMultiNodePropertyMapper())
                .SetNext(new ContentPickerUrlPropertyMapper())
                .SetNext(new MediaPickerPropertyMapper())
                .SetNext(new ChildrenPropertyMapper())
                .SetNext(new SpecialCasePropertyMapper())
                .SetNext(new DefaultPropertyMapper());

            defaultMap = pipe;
        }

        public static IPropertyMapper Default
        {
            get { return defaultMap; }
        }
    }
}