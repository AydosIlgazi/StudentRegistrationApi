namespace StudentRegistration.Domain.Aggregates;

public class Student  : AggregateRoot
{
	private Person _person;
	private string _studentId;
	private Program _program;
	private int _givenCredits;
	private ICollection<StudentRecord> _studentRecords;
	private int _class;
	private float gpa;
	private Semester _startDate;
	private Semester _graduationDate;
	public Student(Person person)
	{
		_person = person;
	}

	//Graduate

	//private CalculateGpa

	//Complete course

	//Check Ennrollment Eligiibity
		//Unpaid Fee 8 dönem
		//
}