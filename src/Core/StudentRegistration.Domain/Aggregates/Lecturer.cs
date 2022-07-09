namespace StudentRegistration.Domain.Aggregates;

public class Lecturer :AggregateRoot
{
	private Person _person;
	private string lecturerId;
	private Department _departmant;
	private ICollection<Lecture> _lectures;

	//accept reject objections
}