using LxDp.Domain.DataModels;

namespace LxDp.Domain.ViewModels;

public class ServerViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ServerIp { get; set; }
    public int ServerPort { get; set; }
    public List<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();

}
