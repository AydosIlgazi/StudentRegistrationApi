namespace StudentRegistration.Domain.ValueObjects;

public class Address: ValueObject {
    public string FullAdress {get; init;}
    public string District { get; init;}
    public string City { get; init;}
    public string Country { get; init;}

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FullAdress;
        yield return District;
        yield return City;
        yield return Country;
    }

}
