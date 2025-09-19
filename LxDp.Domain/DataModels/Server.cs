namespace LxDp.Domain.DataModels;

public class Server : Base
{
    public string Name { get; set; }
    public string Ip { get; set; }
    public int? Port { get; set; }
    public List<User> Users { get; set; } = new List<User>();
    public List<Project> Projects { get; set; } = new List<Project>();
}
