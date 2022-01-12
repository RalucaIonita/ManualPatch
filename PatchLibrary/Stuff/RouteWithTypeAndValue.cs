using System;

namespace PatchLibrary.Stuff
{
    public class RouteWithTypeAndValue : RouteWithType
    {
        public object Value { get; set; }
        public RouteWithTypeAndValue(Type type, string route, object value) : base(type, route)
        {
            Value = value;
        }
    }
}
