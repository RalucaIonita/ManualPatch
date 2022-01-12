using System;

namespace PatchLibrary.Stuff
{
    public class TypeWithFieldName
    {
        public Type Type { get; set; }
        public string FieldName { get; set; }
        public object Value { get; set; }

        public TypeWithFieldName(Type type, string fieldName, string root)
        {
            Type = type;
            FieldName = root + "/" + char.ToLower(fieldName[0]).ToString() + fieldName.Substring(1); ;

        }
    }
}
