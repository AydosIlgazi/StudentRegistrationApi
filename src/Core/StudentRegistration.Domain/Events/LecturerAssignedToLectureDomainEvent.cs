namespace StudentRegistration.Domain.Events;

//update lecturer schedule with new lecture
/*				
    if(lecturer.Schedule.IsScheduleAvailable(ds.Slot,ds.Day)== false)
    {
        throw new Exception ("Lecturer is not available during slot");
    }
*/
public class LecturerAssignedToLectureDomainEvent: INotification
{
    public string LecturerId{get;init;}
    public IReadOnlyCollection<DaySlot> DaySlots {get;init;}
}