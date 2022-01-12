using System.Collections.Generic;

namespace Trials.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string SummaryLong { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectStatusLong { get; set; }
        public List<ProjectType> Types { get; set; }
        public List<GeographyType> Regions { get; set; }
    }
}
