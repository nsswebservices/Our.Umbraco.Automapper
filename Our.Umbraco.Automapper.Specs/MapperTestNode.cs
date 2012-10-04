using System;
using System.Collections.Generic;
using System.Data;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Specs
{
    public class MapperTestNode : INode
    {
        private IDictionary<string, object> propsValues = new Dictionary<string, object>()
                                                              {
                                                                  { "umbracoNaviHide", false},
                                                                  { "heading", "test "},
                                                                  { "metaTitle", "test "},
                                                                  { "metaDescription", "test "},
                                                                  { "metaKeywords", "test "},
                                                              };

        private List<INode> childrenAsList = new List<INode>();

        public IProperty GetProperty(string Alias)
        {
            Console.WriteLine(Alias);
            return new TestProperty(Alias, propsValues[Alias]);
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

        public INode Parent { get; set; }
        public int Id { get; set; }
        public int template { get; private set; }
        public int SortOrder { get; private set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string UrlName { get; private set; }
        public string NodeTypeAlias { get; set; }
        public string WriterName { get; private set; }
        public string CreatorName { get; private set; }
        public int WriterID { get; private set; }
        public int CreatorID { get; private set; }
        public string Path { get; private set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; private set; }
        public Guid Version { get; private set; }
        public string NiceUrl { get; set; }
        public int Level { get; set; }
        public List<IProperty> PropertiesAsList { get; private set; }
        public List<INode> ChildrenAsList
        {
            get { return childrenAsList; }
            private set { childrenAsList = value; }
        }

        public void AddProperty(string key, object val)
        {
            propsValues.Add(key, val);
        }
    }
}