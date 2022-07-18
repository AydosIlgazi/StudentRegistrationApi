namespace StudentRegistration.Domain.ValueObjects;

public class TermVO
{
    public string TermId{get;init;}
    public TermStatus TermStatus{get; init;}
    public bool IsEnrollmentActive{get; init;}
    public TermWeeklySlots LectureDaysAndSlots{get; init;}
}