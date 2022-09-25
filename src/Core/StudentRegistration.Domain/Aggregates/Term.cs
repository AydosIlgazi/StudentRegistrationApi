namespace StudentRegistration.Domain.Aggregates;

public class Term : AggregateRoot
{
	private Semester _semester;
	private TermStatus _status;
	private bool _isEnrollmentActive;
	private TermWeeklySlots _lectureDaysAndSlots;
	private int _slotDuration;

	public TermStatus Status => _status;
	public bool IsEnrollmentActive => _isEnrollmentActive;
	public TermWeeklySlots LectureDaysAndSlots => _lectureDaysAndSlots;
	public Semester Semester => _semester;

	public void StartTerm(){
		if(_status == TermStatus.Active){
			throw new StudentRegistrationDomainException("This term is already started");
		}

		_status = TermStatus.Active;
		_isEnrollmentActive = false;
		
		AddDomainEvent(new TermStartedDomainEvent{
			LectureDays = _lectureDaysAndSlots
		});
	}
	public void EndTerm()
	{
		if(_status == TermStatus.Completed){
			throw new StudentRegistrationDomainException("This term is already ended");
		}
		_status = TermStatus.Completed;

		AddDomainEvent(new TermEndedDomainEvent{
			TermId=base.Id
		});
	}
	public void OpenEnrollment()
	{
		if(_status != TermStatus.Active)
		{
			throw new StudentRegistrationDomainException("Only active terms can be opened to enrollment");
		}
		if(_isEnrollmentActive != false)
		{
			throw new StudentRegistrationDomainException("This term is already open to enrollment");
		}
		_isEnrollmentActive = true;
	}
	public void CloseEnrollment()
	{
		if(_status != TermStatus.Active)
		{
			throw new StudentRegistrationDomainException("Only active terms can be closed to enrollment");
		}
		if(_isEnrollmentActive != true)
		{
			throw new StudentRegistrationDomainException("This term is already close to enrollment");
		}
		_isEnrollmentActive = false;
	}
	public Term (Semester semester, TermWeeklySlots lectureDaysAndSlots)
	{
		_semester= semester;
		_status = TermStatus.Waiting;
		_isEnrollmentActive = false;
		_lectureDaysAndSlots = lectureDaysAndSlots;
	}
	private Term(){ }
}