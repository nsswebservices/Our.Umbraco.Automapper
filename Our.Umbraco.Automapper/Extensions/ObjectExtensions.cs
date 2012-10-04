using System;
using System.Diagnostics;
using System.Reflection;

namespace Our.Umbraco.Automapper.Extensions
{
    [DebuggerStepThrough]
    public static class ObjectExtensions
    {
        public static bool IsNull(this object input)
        {
            return (input == null);
        }

        public static bool IsNotNull(this object input)
        {
            return (input != null);
        }

        public static bool CanBeCastTo<T>(this Type type)
        {
            return typeof(T).CanBeCastTo(type);
        }

        public static bool CanBeCastTo(this Type type1, Type type)
        {
            return type1.IsAssignableFrom(type);
        }

        public static bool CanBeCastTo<T>(this object instance)
        {
            return instance.GetType().CanBeCastTo<T>();
        }

        public static bool CanBeCastTo(this object instance, object baseType)
        {
            return CanBeCastTo(instance.GetType(), baseType.GetType());
        }

        public static bool IsConcrete(this Type type)
        {
            return !type.IsAbstract && !type.IsInterface;
        }

        public static bool IsDelegate(this Type type)
        {
            return type.BaseType.CanBeCastTo<MulticastDelegate>();
        }

        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return Attribute.IsDefined(type, typeof (T));
        }

        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            return Attribute.GetCustomAttribute(type, typeof (T)) as T;
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            return Attribute.GetCustomAttribute(propertyInfo, typeof (T)) as T;
        }

        public static bool HasAttribute<T>(this PropertyInfo propertyInfo)
        {
            return Attribute.IsDefined(propertyInfo, typeof(T));
        }
    }
}