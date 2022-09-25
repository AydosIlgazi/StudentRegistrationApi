namespace StudentRegistration.Domain.Exceptions;

public class StudentRegistrationDomainException : Exception{
    public StudentRegistrationDomainException(string message):base(message)
    {
        
    }

    public StudentRegistrationDomainException(string message, Exception innerException)
    : base(message, innerException)
    { }
}