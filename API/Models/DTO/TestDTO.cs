using API.Models.DTO;

public class TestDTO
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public double? Score { get; set; }
    public string? UserId { get; set; }

    public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();

    // Conditionally include Report if it exists
    public ReportDTO? Report { get; set; }
}
