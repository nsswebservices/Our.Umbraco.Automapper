using System.Reflection;
using umbraco.NodeFactory;
using umbraco.interfaces;

namespace Our.Umbraco.Automapper.Framework.Mappers
{
    public delegate INode ConstructNodeDelegate(int id);

    public abstract class AbstractPropertyMapper : IPropertyMapper
    {
        private IPropertyMapper next = EmptyPropertyMapper.Instance;
        public ConstructNodeDelegate CreateNode = i => new Node(i);

        public IPropertyMapper Next
        {
            get { return next; }
            private set { next = value; }
        }

        public IPropertyMapper SetNext(IPropertyMapper n)
        {
            next = n;

            return next;
        }

        public bool CanPerformAction(PropertyInfo propertyInfo)
        {
            return CanPerformActionCore(propertyInfo);
        }

        protected abstract bool CanPerformActionCore(PropertyInfo propertyInfo);

        public void Map<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo)
        {
            if ( CanPerformAction(propertyInfo) )
            {
                MapCore(dest, source, propertyInfo);
                return;
            }

            Next.Map(dest, source, propertyInfo);
        }

        protected abstract void MapCore<TDestination>(TDestination dest, INode source, PropertyInfo propertyInfo);

        protected virtual INode NodeBuilder(int mediaId)
        {
            return CreateNode(mediaId);
        }
    }
}