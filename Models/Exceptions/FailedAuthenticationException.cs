namespace HAOS.Models.Exceptions;
public class FailedAuthenticationException : Exception
{
    public FailedAuthenticationException(string message) : base(message) { }
}