using System;
using Machine.Specifications;
using Our.Umbraco.Automapper.Attributes;
using Our.Umbraco.Automapper.Mappers;

namespace Our.Umbraco.Automapper.Specs.PropertyMappers
{
    [Subject(typeof(SpecialCasePropertyMapper))]
    public class when_mapping_special_cases_without_attributes
    {
        private static SpecialCasePropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;
        private static MapperTestNode node;
        private static SpecialCaseTestItem target;
        private static DateTime createDate = DateTime.Now;


        private Establish context = () =>
                                        {
                                            mapper = new SpecialCasePropertyMapper();
                                            node = new MapperTestNode()
                                                       {
                                                           Id = 1234,
                                                           NiceUrl = "/expected",
                                                           Name = "Name",
                                                           NodeTypeAlias = "NodeTypeAlias",
                                                           CreateDate = createDate,
                                                           Level = 12,
                                                           Parent = new MapperTestNode() {Name = "Parent Name"}
                                                       };

                                            target = new SpecialCaseTestItem();
                                        };

        private Because of = () =>
                                 {
                                     mapper.Map(target, node, typeof(SpecialCaseTestItem).GetProperty("Url"));
                                     mapper.Map(target, node, typeof(SpecialCaseTestItem).GetProperty("Name"));
                                     mapper.Map(target, node, typeof(SpecialCaseTestItem).GetProperty("Id"));
                                     mapper.Map(target, node, typeof(SpecialCaseTestItem).GetProperty("ParentName"));
                                     mapper.Map(target, node, typeof(SpecialCaseTestItem).GetProperty("NodeTypeAlias"));
                                     mapper.Map(target, node, typeof(SpecialCaseTestItem).GetProperty("CreateDate"));
                                     mapper.Map(target, node, typeof(SpecialCaseTestItem).GetProperty("Level"));
                                 };

        private It should_map_url = () => target.Url.ShouldEqual("/expected");

        private It should_map_name = () => target.Name.ShouldEqual("Name");

        private It should_map_id = () => target.Id.ShouldEqual(1234);

        private It should_map_parentname = () => target.ParentName.ShouldEqual("Parent Name");

        private It should_map_nodetypealias = () => target.NodeTypeAlias.ShouldEqual("NodeTypeAlias");

        private It should_map_createdate = () => target.CreateDate.ShouldEqual(createDate);

        private It should_map_level = () => target.Level.ShouldEqual(12);
    }

    [Subject(typeof(SpecialCasePropertyMapper))]
    public class when_mapping_special_cases_with_attributes
    {
        private static SpecialCasePropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;
        private static MapperTestNode node;
        private static SpecialCaseTestItemWithAttributes target;
        private static DateTime createDate = DateTime.Now;


        private Establish context = () =>
        {
            mapper = new SpecialCasePropertyMapper();
            node = new MapperTestNode()
            {
                Id = 1234,
                NiceUrl = "/expected",
                Name = "Name",
                NodeTypeAlias = "NodeTypeAlias",
                CreateDate = createDate,
                Level = 12,
                Parent = new MapperTestNode() { Name = "Parent Name" }
            };

            target = new SpecialCaseTestItemWithAttributes();
        };

        private Because of = () =>
        {
            mapper.Map(target, node, typeof(SpecialCaseTestItemWithAttributes).GetProperty("Url_"));
            mapper.Map(target, node, typeof(SpecialCaseTestItemWithAttributes).GetProperty("Name_"));
            mapper.Map(target, node, typeof(SpecialCaseTestItemWithAttributes).GetProperty("Id_"));
            mapper.Map(target, node, typeof(SpecialCaseTestItemWithAttributes).GetProperty("ParentName_"));
            mapper.Map(target, node, typeof(SpecialCaseTestItemWithAttributes).GetProperty("NodeTypeAlias_"));
            mapper.Map(target, node, typeof(SpecialCaseTestItemWithAttributes).GetProperty("CreateDate_"));
            mapper.Map(target, node, typeof(SpecialCaseTestItemWithAttributes).GetProperty("Level_"));
        };

        private It should_map_url = () => target.Url_.ShouldEqual("/expected");

        private It should_map_name = () => target.Name_.ShouldEqual("Name");

        private It should_map_id = () => target.Id_.ShouldEqual(1234);

        private It should_map_parentname = () => target.ParentName_.ShouldEqual("Parent Name");

        private It should_map_nodetypealias = () => target.NodeTypeAlias_.ShouldEqual("NodeTypeAlias");

        private It should_map_createdate = () => target.CreateDate_.ShouldEqual(createDate);

        private It should_map_level = () => target.Level_.ShouldEqual(12);
    }

    [Subject(typeof(SpecialCasePropertyMapper))]
    public class when_mapping_special_cases_and_parent_is_null
    {
        private static SpecialCasePropertyMapper mapper;
        private static PropertyMapperTestHelper testHelper;
        private static MapperTestNode node;
        private static SpecialCaseTestParent target;
        private static DateTime createDate = DateTime.Now;


        private Establish context = () =>
        {
            mapper = new SpecialCasePropertyMapper();
            node = new MapperTestNode();

            target = new SpecialCaseTestParent();
        };

        private Because of = () =>
        {
            mapper.Map(target, node, typeof(SpecialCaseTestParent).GetProperty("ParentName"));
            mapper.Map(target, node, typeof(SpecialCaseTestParent).GetProperty("ParentNameAttr"));
        };

        private It should_map_parentname = () => target.ParentName.ShouldEqual(null);

        private It should_map_parentname_mapping_with_attribute = () => target.ParentNameAttr.ShouldEqual(null);
    }

    public class SpecialCaseTestParent
    {
        public string ParentName { get; set; }

        [UmbracoProperty(IsSpecialCase = true, PropertyAlias = "parentname")]
        public string ParentNameAttr { get; set; }
    }

    public class SpecialCaseTestItem
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string ParentName { get; set; }
        public string NodeTypeAlias { get; set; }
        public DateTime CreateDate { get; set; }

        public int Level { get; set; }
    }

    public class SpecialCaseTestItemWithAttributes
    {
        [UmbracoProperty(IsSpecialCase = true, PropertyAlias = "url")]
        public string Url_ { get; set; }

        [UmbracoProperty(IsSpecialCase = true, PropertyAlias = "name")]
        public string Name_ { get; set; }

        [UmbracoProperty(IsSpecialCase = true, PropertyAlias = "id")]
        public int Id_ { get; set; }

        [UmbracoProperty(IsSpecialCase = true, PropertyAlias = "parentname")]
        public string ParentName_ { get; set; }

        [UmbracoProperty(IsSpecialCase = true, PropertyAlias = "nodetypealias")]
        public string NodeTypeAlias_ { get; set; }

        [UmbracoProperty(IsSpecialCase = true, PropertyAlias = "createdate")]
        public DateTime CreateDate_ { get; set; }

        [UmbracoProperty(IsSpecialCase = true, PropertyAlias = "level")]
        public int Level_ { get; set; }
    }
}