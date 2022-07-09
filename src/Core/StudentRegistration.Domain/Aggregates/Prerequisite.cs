namespace StudentRegistration.Domain.Aggregates;

public class Prerequisite :BaseEntity
{
	private Course _course;

	private ICollection<Course> _prerequisites;
}