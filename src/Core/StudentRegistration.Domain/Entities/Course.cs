namespace StudentRegistration.Domain.Entities;

public class Course : BaseEntity {
	private string _code;
	private int _credit;
	private string _name;
	public Course(string code, int credit, string name)
	{
		_code = code;
		_credit = credit;
		_name = name;
	}
}