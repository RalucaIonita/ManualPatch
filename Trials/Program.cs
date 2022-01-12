using System;
using System.Collections.Generic;
using System.Linq;
using Trials.Entities;
using Trials.Stuff;

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

            var routes = new List<string>();


            project.GetType().GetAllTypesWithSubObjects(ref routes, "");

            for (var i = 0; i < routes.Count(); i++)
            {
                Console.WriteLine(routes[i]);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
