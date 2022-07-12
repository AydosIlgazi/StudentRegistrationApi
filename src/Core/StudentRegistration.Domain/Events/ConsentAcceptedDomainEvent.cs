namespace StudentRegistration.Domain.Events;

//enroll studen to the lecture
public class ConsentAcceptedDomainEvent : INotification
{
    public string LectureId{get;init;}
}