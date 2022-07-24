namespace StudentRegistration.Domain.Aggregates;

public class Consent : AggregateRoot
{
	private string _lectureId;
	private string _studentId;
	private ConsentStatus _consentStatus;

	public ConsentStatus ConsentStatus => _consentStatus;

	public Consent(string lectureId, string studentId)
	{
		_lectureId = lectureId;
		_studentId = studentId;
		_consentStatus = ConsentStatus.Waiting;
	}
	
	public void AcceptConsent()
	{
		if(_consentStatus != ConsentStatus.Waiting){
			throw new Exception ("Consent is already decided");
		}
		_consentStatus = ConsentStatus.Accepted;
		AddDomainEvent(new ConsentAcceptedDomainEvent{
			LectureId = _lectureId
		});
	}
	public void RejectConsent()
	{
		if(_consentStatus != ConsentStatus.Waiting){
			throw new Exception ("Consent is already decided");
		}
		_consentStatus = ConsentStatus.Rejected;
	}
}