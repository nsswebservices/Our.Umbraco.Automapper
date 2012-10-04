using AutoMapper;

namespace Our.Umbraco.Automapper.Extensions
{
    public static class MapperExtentions
    {
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) return default(T);

            return (T)Mapper.Map(obj, obj.GetType(), typeof(T));
        }

        public static T MapTo<T>(this object obj, T dest)
        {
            if (obj == null) return default(T);

            return (T)Mapper.Map(obj, dest, obj.GetType(), typeof(T));
        }
    }
}