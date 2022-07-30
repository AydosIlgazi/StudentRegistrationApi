namespace StudentRegistration.Domain.Exceptions;

public class StudentRegistrationDomainException : Exception{
    public StudentRegistrationDomainException(string message):base(message)
    {
        
    }
}