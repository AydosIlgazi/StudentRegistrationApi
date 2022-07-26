namespace StudentRegistration.Domain.Events;

//update student schedule
public class StudentEnrollmentCreatedDomainEvent : INotification
{
    public string StudentId {get; init;}
    public int LectureId{get; init;}
}