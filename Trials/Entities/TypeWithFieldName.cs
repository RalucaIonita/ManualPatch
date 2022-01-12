using System;

namespace Trials.Entities
{
    public class TypeWithFieldName
    {
        public Type Type { get; set; }
        public string FieldName { get; set; }

        public TypeWithFieldName(Type type, string fieldName)
        {
            Type = type;
            FieldName = "/" + char.ToLower(fieldName[0]).ToString() + fieldName.Substring(1); ;
        }
    }
}
