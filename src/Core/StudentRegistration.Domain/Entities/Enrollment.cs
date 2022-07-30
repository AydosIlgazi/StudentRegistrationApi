namespace StudentRegistration.Domain.Entities;

public class Enrollment : BaseEntity
{
    private int _lectureId;
    private EnrollmentStatus _enrollmentStatus;

    public int LectureId =>_lectureId;
    public EnrollmentStatus EnrollmentStatus =>_enrollmentStatus;

    public Enrollment(LectureVO lecture , TermVO term, int currentEnrollmentCount)
    {
        if(term.IsEnrollmentActive==false)
        {
            throw new StudentRegistrationDomainException("Enrollment for this term is not available");
        }
        if(currentEnrollmentCount >= lecture.Capacity)
        {
            throw new StudentRegistrationDomainException("Lecture is full");
        }
        _lectureId = lecture.LectureId;
        _enrollmentStatus = EnrollmentStatus.Active;
    }
    public void ApproveEnrollment()
    {
        if(_enrollmentStatus != EnrollmentStatus.Active){
            throw new StudentRegistrationDomainException("Only active enrollments can be completed");
        }
        _enrollmentStatus = EnrollmentStatus.Completed;
    }
    public void CancelEnrollment()
    {
        		
        if(_enrollmentStatus == EnrollmentStatus.Left)
		{
			throw new StudentRegistrationDomainException("You are not enrolled this lecture");
		}
		if(_enrollmentStatus ==EnrollmentStatus.Completed)
		{
			throw new StudentRegistrationDomainException("This enrollment completed");
		}
        _enrollmentStatus = EnrollmentStatus.Left;
    }
}