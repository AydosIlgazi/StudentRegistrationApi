namespace StudentRegistration.Domain.Aggregates;

public class Department  : AggregateRoot
{
	private string _name;
	private Building _mainBuilding;
	private ICollection<Program> _programs;

}