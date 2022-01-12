namespace Trials.Entities
{
    public class ProjectType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SomeOtherStuff ceva {get; set;}
    }

    public class SomeOtherStuff
    {
        public string Name { get; set; }
    }
}
