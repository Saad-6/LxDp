namespace LxDp.Domain.ViewModels;

public class ScriptViewModel
{
    public int Id { get; set; }
    public int Order { get; set; }
    public bool RunAfterPublishing { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
}
