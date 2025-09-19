namespace LxDp.Domain;

public class Response<T> where T : class
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; } = null;
}
