using PatchLibrary.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PatchLibrary.Stuff
{
    public static class ReflectionMethods
    {
        //v1 - dead
        public static List<string> GetAllFields<T>(this T payload)
        {
            var fieldNames = new List<string>();
            var type = typeof(T);
            var fields = type.GetFields();

            var rawFieldNames = type.GetProperties().Select(x => x.Name).ToList();

            foreach (var fieldName in rawFieldNames)
            {
                var newFieldName = '/'.ToString() + char.ToLower(fieldName[0]).ToString() + fieldName.Substring(1);
                fieldNames.Add(newFieldName);
            }

            return fieldNames;
        }

        public static List<string> GetAllTypesSimple<T>(this T payload)
        {
            var fieldNames = new List<string>();
            var type = typeof(T);
            var fields = type.GetFields();

            var fieldTypes = type.GetProperties().Select(x => x.PropertyType.Name.ToLower()).ToList();

            return fieldTypes;
        }


        public static List<string> GetAllTypesForGeneric<T>(this T payload)
        {
            var typeNames = new List<string>();
            var type = typeof(T);
            var properties = type.GetProperties().Select(x => x.PropertyType).ToList();

            foreach (var property in properties)
            {
                var name = property.Name;

                if (property.GenericTypeArguments.Count() != 0)
                {
                    var subObjType = property.GenericTypeArguments.First().Name;

                    name = name.Replace("`1", "") + "<" + subObjType + ">";
                }
                typeNames.Add(name);
            }
            return typeNames;
        }

        public static void GetAllTypesWithSubObjects(this Type type, ref List<string> routes, string root)
        {
            if (!type.IsClass && !type.IsEnum)
                return;

            var propertiesWithFieldNames = type.GetProperties().Select(x => new TypeWithFieldName(x.PropertyType, x.Name, root)).ToList();

            foreach (var propertyWithFieldName in propertiesWithFieldNames)
            {
                if (propertyWithFieldName.Type == typeof(string) || !propertyWithFieldName.Type.IsClass)
                {
                    var newRoute = char.ToLower(propertyWithFieldName.FieldName[0]).ToString() + propertyWithFieldName.FieldName.Substring(1);
                    routes.Add(newRoute);
                }
                else
                {
                    //Console.WriteLine($"{propertyWithFieldName.Type.Name}");

                    if (propertyWithFieldName.Type.GenericTypeArguments.Count() != 0)
                    {
                        var subObjType = propertyWithFieldName.Type.GenericTypeArguments.First();

                        propertyWithFieldName.Type = subObjType;
                    }
                    propertyWithFieldName.Type.GetAllTypesWithSubObjects(ref routes, propertyWithFieldName.FieldName);
                }
            }
        }

        public static bool HasOnlyBasicFields(this Type type)
        {
            var propertiesTypes = type.GetProperties().Select(x => x.PropertyType);
            var propertiesTypesClasses = propertiesTypes.Where(x => x != typeof(string) || x.IsClass).Count();

            return propertiesTypesClasses == propertiesTypes.Count();
        }

        public static List<string> GetSimpleFiledsRoutes(this Type type)
        {
            var routes = new List<string>();
            var propertiesWithFieldNames = type.GetProperties().Select(x => new TypeWithFieldName(x.PropertyType, x.Name, "")).ToList();

            foreach (var propertyWithFieldName in propertiesWithFieldNames)
            {
                if (propertyWithFieldName.Type == typeof(string) || !propertyWithFieldName.Type.IsClass)
                {
                    var newRoute = char.ToLower(propertyWithFieldName.FieldName[0]).ToString() + propertyWithFieldName.FieldName.Substring(1);
                    routes.Add(newRoute);
                }
            }
            return routes;
        }

        public static void GetObjectFieldsRoutes(this Type type, ref List<string> routes, string root)
        {
            if (!type.IsClass && !type.IsEnum)
                return;

            var propertiesWithFieldNames = type.GetProperties().Select(x => new TypeWithFieldName(x.PropertyType, x.Name, root)).ToList();

            foreach (var propertyWithFieldName in propertiesWithFieldNames)
            {
                if (propertyWithFieldName.Type == typeof(string) || !propertyWithFieldName.Type.IsClass)
                {
                    var newRoute = char.ToLower(propertyWithFieldName.FieldName[0]).ToString() + propertyWithFieldName.FieldName.Substring(1);
                    routes.Add(newRoute);
                }
                else
                {
                    if (propertyWithFieldName.Type.GenericTypeArguments.Count() == 0)
                        propertyWithFieldName.Type.GetAllTypesWithSubObjects(ref routes, propertyWithFieldName.FieldName);
                }
            }
        }

        public static Type GetTypeFromRoute<T>(this T entity, string route)
        {
            var type = entity.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var propertyName = new TypeWithFieldName(property.PropertyType, property.Name, "");
                if (route == propertyName.FieldName)
                    return propertyName.Type;
                if (!route.StartsWith(propertyName.FieldName))
                    continue;

                //then it is an object
                if (route.Any(char.IsDigit))
                {
                    var digits = "/" + new String(route.Where(c => char.IsDigit(c)).ToArray());
                    route = route.Replace(digits, "");

                }
                return GetTypeFromRoute(entity, route);
            }
            return null;

        }

        public static void GetTypeFromRoute(this Type type, ref List<RouteWithType> routes, string root, string route)
        {
            if (route.EndsWith("/"))
                route = route.Substring(0, route.Length - 1);
            if (!type.IsClass && !type.IsEnum)
                return;

            if (route.Any(char.IsDigit))
            {
                var digits = "/" + new String(route.Where(c => char.IsDigit(c)).ToArray());
                route = route.Replace(digits, "");
            }

            var propertiesWithFieldNames = type.GetProperties().Select(x => new TypeWithFieldName(x.PropertyType, x.Name, root)).ToList();

            foreach (var propertyWithFieldName in propertiesWithFieldNames)
            {
                if (route == propertyWithFieldName.FieldName)
                {
                    routes.Add(new RouteWithType(propertyWithFieldName.Type, propertyWithFieldName.FieldName));
                    return;
                }
                if (propertyWithFieldName.Type == typeof(string) || !propertyWithFieldName.Type.IsClass)
                {
                    var newRoute = char.ToLower(propertyWithFieldName.FieldName[0]).ToString() + propertyWithFieldName.FieldName.Substring(1);
                    if (route == newRoute)
                    {
                        routes.Add(new RouteWithType(propertyWithFieldName.Type, newRoute));
                        return;
                    }
                }
                else
                {

                    if (propertyWithFieldName.Type.GenericTypeArguments.Count() != 0)
                    {
                        var subObjType = propertyWithFieldName.Type.GenericTypeArguments.First();
                        propertyWithFieldName.Type = subObjType;
                    }
                    propertyWithFieldName.Type.GetTypeFromRoute(ref routes, propertyWithFieldName.FieldName, route);
                }
            }

        }
    }
}
