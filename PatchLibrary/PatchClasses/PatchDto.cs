using PatchLibrary.Stuff;
using System.Collections.Generic;
using System.Linq;

namespace PatchLibrary.PatchClasses
{
    public class PatchDto<T> where T : new()
    {
        public List<PatchOperation<T>> Operations { get; set; }

        public void ApplyTo(ref T entity)
        {
            //validate
            ValidatePaths(entity);
            foreach (var operation in Operations)
            {
                operation.Apply(ref entity);
            }



        }

        private void ValidatePaths(T entity)
        {
            //check not existent routes
            var entityType = typeof(T);
            var allPaths = Operations.Select(o => o.Path).ToList();

            //simple
            var simpleRoutes = entityType.GetSimpleFiledsRoutes();
            allPaths = allPaths.Except(simpleRoutes).ToList();

            //object paths
            var objectRoutes = new List<string>();
            entityType.GetObjectFieldsRoutes(ref objectRoutes, "");
            allPaths = allPaths.Except(objectRoutes).ToList();

            //todo: check for lists here

        }
    }
}
