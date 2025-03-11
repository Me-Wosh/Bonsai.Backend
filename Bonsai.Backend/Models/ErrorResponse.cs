namespace Bonsai.Backend.Models;

public class Error
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class ErrorResponse
{
    public required Error Error { get; set; }
}