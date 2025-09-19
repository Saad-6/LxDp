namespace LxDp.Domain.DataModels;

public class Script : Base
{
    public int Order { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }

}
