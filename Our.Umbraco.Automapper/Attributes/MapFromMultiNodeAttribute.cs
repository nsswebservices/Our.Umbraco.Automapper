using System;

namespace Our.Umbraco.Automapper.Attributes
{
    public class MapFromMultiNodeAttribute : Attribute
    {
        private NodeTypes type = NodeTypes.Node;
        public NodeTypes Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}