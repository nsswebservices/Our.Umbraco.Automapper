using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Models;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.web;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Mappers
{
    public delegate IPublishedContent ConstructNodeDelegate(int id);

    public abstract class AbstractPropertyMapper : IPropertyMapper
    {
        public ConstructNodeDelegate CreateNode = i => new StormPublishedContent(new Node(i));

        public bool CanPerformAction(PropertyInfo propertyInfo)
        {
            return CanPerformActionCore(propertyInfo);
        }

        protected abstract bool CanPerformActionCore(PropertyInfo propertyInfo);

        public void Map<TDestination>(TDestination dest, IPublishedContent source, PropertyInfo propertyInfo)
        {
            if ( CanPerformAction(propertyInfo) )
            {
                MapCore(dest, source, propertyInfo);
            }
        }

        protected abstract void MapCore<TDestination>(TDestination dest, IPublishedContent source, PropertyInfo propertyInfo);

        protected virtual IPublishedContent NodeBuilder(int mediaId)
        {
            return CreateNode(mediaId);
        }
    }

    public class StormPublishedContent : PublishedContentBase
    {
        private readonly INode node;

        public StormPublishedContent(INode node)
        {
            this.node = node;
        }

        public override IPublishedContentProperty GetProperty(string alias)
        {
            return new StormPublishedContentProperty(node.GetProperty(alias));
        }

        public override int Id
        {
            get { return node.Id; }
        }

        public override int TemplateId
        {
            get { return node.template; }
        }

        public override int SortOrder
        {
            get { return node.SortOrder; }
        }

        public override string Name
        {
            get { return node.Name; }
        }

        public override string UrlName
        {
            get { return node.UrlName; }
        }

        public override string DocumentTypeAlias
        {
            get { return node.NodeTypeAlias; }
        }

        public override int DocumentTypeId
        {
            get { throw new NotImplementedException(); }
        }

        public override string WriterName
        {
            get { return node.WriterName; }
        }

        public override string CreatorName
        {
            get { return node.CreatorName; }
        }

        public override int WriterId
        {
            get { return node.WriterID; }
        }

        public override int CreatorId
        {
            get { return node.CreatorID; }
        }

        public override string Path
        {
            get { return node.Path; }
        }

        public override DateTime CreateDate
        {
            get { return node.CreateDate; }
        }

        public override DateTime UpdateDate
        {
            get { return node.UpdateDate; }
        }

        public override Guid Version
        {
            get { return node.Version; }
        }

        public override int Level
        {
            get { return node.Level; }
        }

        public override IEnumerable<IPublishedContent> Children
        {
            get { return node.ChildrenAsList.Select(x => new StormPublishedContent(x)); }
        }

        public override ICollection<IPublishedContentProperty> Properties
        {
            get { return node.PropertiesAsList.Select(x => new StormPublishedContentProperty(x)).ToArray(); }
        }

        public override PublishedItemType ItemType
        {
            get { return PublishedItemType.Content; }
        }

        public override IPublishedContent Parent
        {
            get
            {
                if (node.Parent == null) return null;

                return new StormPublishedContent(node.Parent);
            }
        }
    }

    public class StormPublishedContentProperty : IPublishedContentProperty
    {
        private readonly IProperty property;

        public string Alias
        {
            get { return property.Alias; }
        }

        public object Value
        {
            get { return property.Value; }
        }

        public Guid Version
        {
            get { return property.Version; }
        }

        public StormPublishedContentProperty(IProperty property)
        {
            this.property = property;
        }
    }
}