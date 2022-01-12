using System;
using System.Collections.Generic;
using System.Linq;
using Trials.Entities;

namespace Trials.Stuff
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
                var newFieldName =  '/'.ToString() + char.ToLower(fieldName[0]).ToString() + fieldName.Substring(1);
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

            foreach(var property in properties)
            {
                var name = property.Name;

                if(property.GenericTypeArguments.Count() != 0)
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

            foreach(var propertyWithFieldName in propertiesWithFieldNames)
            {
                if (propertyWithFieldName.Type == typeof(string) || !propertyWithFieldName.Type.IsClass)
                {
                    var newRoute = 
                        //root + 
                        char.ToLower(propertyWithFieldName.FieldName[0]).ToString() + propertyWithFieldName.FieldName.Substring(1);
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



    }
}
