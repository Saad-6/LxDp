using LxDp.Domain.DataModels;

namespace LxDp.Domain.ViewModels;

public class PublishModel
{
    public Project Project { get; set; } 
    public IList<FileStream> Files { get; set; } = new List<FileStream>();
}
