namespace LxDp.Domain.DataModels;

public class Project : Base
{
    public string Name { get; set; }
    public string PublishFolder { get; set; }
    public string RootDirectory { get; set; }
    public ProjectConfiguration Configuration { get; set; }
    public int ServerId { get; set; }
    public List<Script> Scripts { get; set; } = new List<Script>();
   
}
