namespace StudentRegistration.Domain.Aggregates;

public class CoursePlan : BaseEntity
{
	private SemesterType _semesterType;
	private Course _course;
	private int _term;
}