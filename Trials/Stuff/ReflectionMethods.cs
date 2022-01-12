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

        public static List<string> GetAllTypes<T>(this T payload)
        {
            var fieldNames = new List<string>();
            var type = typeof(T);
            var fields = type.GetFields();

            var fieldTypes = type.GetProperties().Select(x => x.PropertyType.Name).ToList();

            return fieldTypes;
        }

        //public static List<string> GetAllFieldsWithSubobjects<T>(this T payload)
        //{
        //    var fieldNames = new List<string>();
        //    var type = typeof(T);
        //    var fields = type.GetFields();

            

        //    foreach(var fieldType in fieldTypes)
        //    {
        //        if (fieldType.Name)
        //    }
            


        //}




        //public static List<string> GetAllFields<T>(this T payload)
        //{

        //}


    }
}
