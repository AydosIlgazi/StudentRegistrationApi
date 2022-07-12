namespace StudentRegistration.Domain.ValueObjects;

public class DaySlot :ValueObject
{
    public Day Day {get; init;}
    public Slot Slot{get; init;}
    public bool IsAvailable{get; init;};


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Day;
        yield return Slot;
    }
}