namespace StudentRegistration.Domain.Aggregates;

public class StudentRecord :BaseEntity
{
	private Lecture _lecture;
	private Grade _grade;

	public StudentRecord(Lecture lecture, Grade grade= Enums.Grade.None)
	{
		_lecture = lecture;
		_grade = grade;
	}

	public void SetNewGrade(Grade grade)
	{
		_grade = grade;
	}
	public Grade Grade => _grade;
}