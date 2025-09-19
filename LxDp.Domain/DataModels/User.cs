namespace LxDp.Domain.DataModels;

public class User : Base
{
    public Server Server { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
