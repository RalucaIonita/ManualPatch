using System;
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

            for (var i = 0; i < types.Count(); i++)
            {
                Console.WriteLine(types[i] + " " + typesFull[i]+ " " + fields[i]);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
