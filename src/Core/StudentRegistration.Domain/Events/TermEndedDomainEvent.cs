namespace StudentRegistration.Domain.Events;

// make schedules is active false
public class TermEndedDomainEvent : INotification
{
    public int TermId {get;init;}

}