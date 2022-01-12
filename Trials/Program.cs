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
            var types = project.GetAllTypes();

            for (var i = 0; i < types.Count(); i++)
            {
                Console.WriteLine(types[i] + " " + fields[i]);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
