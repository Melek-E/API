using API.Models.DTO;

public class ReportDTO
{
    public int Id { get; set; }
    public double Score { get; set; }
    public List<QuestionAssessmentDTO> QuestionAssessments { get; set; } = new List<QuestionAssessmentDTO>();

    public int TestId { get; set; }

    public string AdminId { get; set; }
}
