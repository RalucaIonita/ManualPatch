using System.Collections.Generic;
using System.Linq;

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
            var fields = type.GetFields();
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

    }
}
