using System;

namespace PatchLibrary.Stuff
{
    public class RouteWithType
    {
        public Type Type { get; set; }
        public string Route { get; set; }

        public RouteWithType(Type type, string route)
        {
            Type = type;
            Route = route;
        }
    }
}
