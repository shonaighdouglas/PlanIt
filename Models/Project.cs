namespace PlanIt.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectType { get; set; }
        public string? PatternSource { get; set; }
        public string? PatternSourceURL { get; set; }
        public float? PatternCost { get; set; }
        public string? ProjectThumbnail { get; set; }
        public DateOnly DateAdded { get; set; }
        public DateOnly? DateStarted { get; set; }
        public DateOnly? DateCompleted { get; set; }
    }
}
