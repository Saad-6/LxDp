using LxDp.Domain.DataModels;

namespace LxDp.Domain.ViewModels;

public class CreateServerDto
{
    public string ServerName { get; set; }
    public string ServerIp { get; set; }
    public int? ServerPort { get; set; }
    public User RootUser { get; set; }
    public List<User> Users { get; set; } = new List<User>();

}
