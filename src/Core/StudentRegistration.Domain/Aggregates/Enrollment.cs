namespace StudentRegistration.Domain.Aggregates;

public class Enrollment : AggregateRoot
{
	private Lecture _lecture;
	private ICollection<Student> _student;
}