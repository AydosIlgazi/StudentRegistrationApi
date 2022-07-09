namespace StudentRegistration.Domain.ValueObjects;

public class FullName : ValueObject {
	public string Name {get; init;}
    public string Surname { get; init; }

	public string GetFullName => Name + ' '+ Surname;
	protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
    }
}