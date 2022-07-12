namespace StudentRegistration.Domain.Events;

//update lecturer schedule with new lecture
public class LecturerAssignedToLectureDomainEvent: INotification
{
    public string LecturerId{get;init;}
    public IReadOnlyCollection<DaySlot> DaySlots {get;init;}
}