using PatchLibrary.Stuff;
using System;
using System.Collections.Generic;

namespace PatchLibrary.PatchClasses
{
    public class PatchOperation<T> where T : new()
    {
        public string Operation { get; set; }
        public string Path { get; set; }
        public object Value { get; set; }

        public void Apply(ref T entity)
        {
            switch (Operation)
            {
                case "add":
                    ApplyAdd(ref entity);
                    break;
                case "replace":
                    ApplyReplace(ref entity);
                    break;
                case "delete":
                    ApplyRemove(ref entity);
                    break;
                default:
                    throw new NotSupportedException("Operation not supported.");
            }
        }

        private void ApplyAdd(ref T entity)
        {
            //validare
            var details = new List<RouteWithType>();
            entity.GetType().GetTypeFromRoute(ref details, "", Path);
            var type = details[0].Type;
            if (type != typeof(List<>))
                throw new NotSupportedException("Operation cannot be done on this type of object");


        }

        private void ApplyReplace(ref T entity)
        {
            var newEntity = new T();
        }

        private void ApplyRemove(ref T entity)
        {
            var newEntity = new T();
        }
    }
}
