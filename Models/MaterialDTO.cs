namespace PlanIt.Models
{
    public class MaterialDTO
    {
        public long Id { get; set; }
        public string? MatName { get; set; }
        public string? MatSource { get; set; }
        public string? MatSourceURL { get; set; }
        public float? MatCost { get; set; }
        public string? MatImage { get; set; }
        public bool IsSelected { get; set; }
    }
}
