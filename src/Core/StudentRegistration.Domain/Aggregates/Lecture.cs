namespace StudentRegistration.Domain.Aggregates;

public class Lecture :AggregateRoot
{
	private Lecturer _lecturer;
	private Classroom _classroom;
	private Term _term;
	private Course _course;

	public Lecture()
	{
		
	}

	
}