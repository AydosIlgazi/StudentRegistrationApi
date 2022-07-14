namespace StudentRegistration.Domain.ValueObjects;

public class TermVO
{
    public string TermId{get;}
    public TermStatus TermStatus{get;}
    public bool IsEnrollmentActive{get;}

    public TermWeeklySlots LectureDaysAndSlots{get;}
}