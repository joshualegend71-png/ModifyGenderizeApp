namespace ModifyGenderizeApp.Domain
{
    public class ClassifyResultDto
    {
        public string Name { get; set; } = default!;
        public string? Gender { get; set; } = default!;
        public double Probability { get; set; }
        public int SampleSize { get; set; }
        public bool IsConfident { get; set; }
        public string ProcessedAt { get; set; } = default!;
    }
}
