namespace StudentRegistration.Domain.Events;

//reservelsot for th lecture
public class SectionAddedToLectureDomainEvent: INotification
{
    public IReadOnlyCollection<DaySlot> DaySlots {get; init;}
}