namespace StudentRegistration.Domain.Events;

//update student schedule
public class StudentEnrollmentCanceledDomainEvent : INotification
{
    public string StudentId {get; init;}
    public string LectureId{get; init;}
}