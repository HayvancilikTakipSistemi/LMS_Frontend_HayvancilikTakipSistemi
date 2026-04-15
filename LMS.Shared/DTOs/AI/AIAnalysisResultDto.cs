namespace LMS.Shared.DTOs.AI;

public class AIAnalysisResultDto
{
    public int AnimalId { get; set; }
    public string HealthStatusDescription { get; set; } = string.Empty;
    public string ProductivityPrediction { get; set; } = string.Empty;
    public string ActionRequired { get; set; } = string.Empty;
    public decimal RiskScore { get; set; } // 0.0 to 10.0
    public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;
}
