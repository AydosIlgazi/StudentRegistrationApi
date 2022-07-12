namespace StudentRegistration.Domain.Events;

//update student schedule
public class StudentEnrollmentCompletedDomainEvent : INotification
{
    public string StudentId {get; init;}
    public string LectureId{get; init;}
}