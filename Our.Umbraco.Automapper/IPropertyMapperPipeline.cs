using umbraco.interfaces;

namespace Our.Umbraco.Automapper
{
    public interface IPropertyMapperPipeline<in T>
    {
        void Map(T destination, INode source);
    }
}