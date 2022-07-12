namespace StudentRegistration.Domain.Events;

// add this lecture days to all schedules list as new schedule
public class TermStartedDomainEvent : INotification
{
    public IReadOnlyCollection<TermDailySlots> LectureDays {get;init;}

}