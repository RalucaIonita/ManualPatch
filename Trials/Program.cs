using PatchLibrary.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using Trials.Entities;

namespace Trials
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var project = new Project();
            var fields = project.GetAllFields();
            var types = project.GetAllTypesSimple();
            var typesFull = project.GetAllTypesForGeneric();

            var routes = new List<RouteWithType>();

            //var type = project.GetTypeFromRoute("/regions/");


            project.GetType().GetTypeFromRoute(ref routes, "", "/regions/");

            for (var i = 0; i < routes.Count(); i++)
            {
                Console.WriteLine(routes[i].Type.Name + " " + routes[i].Route);
            }

            //var 

            //Console.WriteLine($"Type: {type.Name}");

            Console.WriteLine("Hello World!");
        }
    }
}
