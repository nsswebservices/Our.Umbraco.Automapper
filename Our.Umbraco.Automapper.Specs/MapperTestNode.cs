using System;
using System.Collections.Generic;
using Umbraco.Core.Models;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Specs
{
    public class MapperTestNode : IPublishedContent
    {
        private IDictionary<string, object> propsValues = new Dictionary<string, object>()
                                                              {
                                                                  { "umbracoNaviHide", false},
                                                                  { "heading", "test "},
                                                                  { "metaTitle", "test "},
                                                                  { "metaDescription", "test "},
                                                                  { "metaKeywords", "test "},
                                                              };

        private List<IPublishedContent> childrenAsList = new List<IPublishedContent>();

        public IPublishedContentProperty GetProperty(string alias)
        {
            if (!propsValues.ContainsKey(alias))
            {
                return new TestProperty(alias, null);
            }

            return new TestProperty(alias, propsValues[alias]);
        }

        public int Id { get; set; }
        public int TemplateId { get; private set; }
        public int SortOrder { get; private set; }
        public string Name { get; set; }
        public string UrlName { get; private set; }
        public string DocumentTypeAlias { get; set; }
        public int DocumentTypeId { get; private set; }
        public string WriterName { get; private set; }
        public string CreatorName { get; private set; }
        public int WriterId { get; private set; }
        public int CreatorId { get; private set; }
        public string Path { get; private set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; private set; }
        public Guid Version { get; private set; }
        public int Level { get; set; }
        public string Url { get; set; }
        public PublishedItemType ItemType { get; private set; }
        public IPublishedContent Parent { get; set; }
        public IEnumerable<IPublishedContent> Children { get; set; }
        public ICollection<IPublishedContentProperty> Properties { get; private set; }

        public object this[string propertyAlias]
        {
            get { throw new NotImplementedException(); }
        }

        public void AddProperty(string key, object val)
        {
            propsValues.Add(key, val);
        }
    }
}