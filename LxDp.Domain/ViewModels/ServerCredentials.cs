namespace LxDp.Domain.ViewModels;

public class ServerCredentials
{
    public string Host { get; set; }
    public int Port { get; set; } = 22;
    public string Username { get; set; }
    public string Password { get; set; }
}
