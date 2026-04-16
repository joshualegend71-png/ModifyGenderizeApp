namespace ModifyGenderizeApp.Domain
{
    public class ClassifyResponseDto
    {
        public string Status { get; set; } = "success";
        public object Data { get; set; } = default!;
    }
}
