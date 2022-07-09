namespace StudentRegistration.Domain.Aggregates;

public class GradeObjection : BaseEntity
{
	private ObjectionStatus _objectionStatus;

	private StudentRecord _studentRecord;

	public GradeObjection(StudentRecord studentRecord)
	{
		_objectionStatus =ObjectionStatus.Waiting;
		_studentRecord = studentRecord;
	}
	
	public void UpdateObjectionStatus(ObjectionStatus newStatus, Grade? newGrade)
	{
		if(newStatus == ObjectionStatus.Waiting)
		{
			//throw exception
		}
		if(newStatus == ObjectionStatus.Accepted)
		{
			_studentRecord.SetNewGrade(newGrade.Value);
			_objectionStatus = ObjectionStatus.Accepted;
		}
		else{
			_objectionStatus = ObjectionStatus.Rejected;
		}
	}
}