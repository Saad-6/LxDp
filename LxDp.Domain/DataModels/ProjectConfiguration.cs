using LxDp.Domain.Enums;

namespace LxDp.Domain.DataModels;

public class ProjectConfiguration : Base
{
   public List<FileTypes> FilesToExclude { get; set; }

}
