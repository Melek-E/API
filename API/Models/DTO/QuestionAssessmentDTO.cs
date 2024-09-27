public class QuestionAssessmentDTO
{
    public int Id { get; set; }
    public bool IsCorrect { get; set; }
    public int PointsAwarded { get; set; }
    public int QuestionId { get; set; }

    public int ReportId { get; set; }
    public string Feedback { get; set; }
}




