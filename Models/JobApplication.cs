namespace JobTracker.Models;

public record JobApplication(int Id, string Company, string Role, string Status);

public record CreateJobApplicationRequest(string Company, string Role, string Status); 