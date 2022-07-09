namespace StudentRegistration.Domain.Aggregates;

public class Program  :BaseEntity
{
	private string _code;
	private int _requiredCreditsToComplete;
	private string _name;
	private string _description;
	private List<Course> _mandotoryCourses;
	private List<Course> _optionalCourses; 
	private Department _department;

	private List<CoursePlan> _programPlan;

	//add prereq
}