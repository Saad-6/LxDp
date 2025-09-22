namespace LxDp.Domain.DataModels;

public class Deployment : Base
{
    public bool IsSuccessful { get; set; }
    public DateTime DeployedAt { get; set; }
    public Project Project { get; set; }
    public string Version { get; set; }
    public string Notes { get; set; }

}
