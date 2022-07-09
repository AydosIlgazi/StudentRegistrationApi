namespace StudentRegistration.Domain.ValueObjects;

public class Semester  :ValueObject
{
	public SemesterType SemesterType{get; init;}
	public int Year{get; init;}

	protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SemesterType;
        yield return Year;
    }
}