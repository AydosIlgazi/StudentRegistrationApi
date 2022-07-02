namespace StudentRegistration.Domain.ValueObjects;

public class Address: ValueObject {
    public string FullAdress {get; }
    public string District { get;}
    public string City { get; }
    public string Country { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FullAdress;
        yield return City;
        yield return Country;
    }
}