
namespace StudentRegistration.Application.Exceptions;

public class ValidationException<T> : Exception
{
    public List<T> Errors { get; set; }
    public ValidationException(string message) : base(message)
    {

    }

    public ValidationException(List<T> errors) 
    {
        Errors = errors;
    }
}
