namespace StudentRegistration.Domain.Aggregates;

public class Term : AggregateRoot
{
	private Semester _semester;
	private TermStatus _status;
	private ICollection<Lecture> _lectures;
	private bool _isEnrollmentActive;

	public void StartTerm(){
		_status = TermStatus.Active;
		_isEnrollmentActive = false;
		//Throw new term started event
	}
	public void EndTerm()
	{
		if(_status == TermStatus.Completed){
			//throw ex
		}
		if(_isEnrollmentActive == true){
			//throw ex
		}
		_status = TermStatus.Completed;
		//Throw term ended
	}
	public void OpenEnrollment()
	{
		if(_status != TermStatus.Active)
		{
			//throw ex
		}
		if(_isEnrollmentActive != false)
		{
			//throw ex
		}
		_isEnrollmentActive = true;
		//throw enrollment started event
	}
	public void CloseEnrollment()
	{
		if(_status != TermStatus.Active)
		{
			//throw ex
		}
		if(_isEnrollmentActive != true)
		{
			//throw ex
		}
		_isEnrollmentActive = false;
		//throw enrollment closed event
	}
	public Term (Semester semester)
	{
		_semester= semester;
		_status = TermStatus.Waiting;
		_isEnrollmentActive = false;
	}
}