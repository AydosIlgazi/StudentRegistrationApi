namespace StudentRegistration.Domain.ValueObjects;

public class SlotTime:ValueObject
{
    public int Hour {get; init;}
    public int Miniute {get; init;}
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hour;
        yield return Miniute;
    }

    public int CompareTo(SlotTime other)
    {
        if(other == null)
            return 1;
        
        if(this.Hour > other.Hour)
            return 1;
        else if(this.Hour< other.Hour)
            return -1;
        else {
            if(this.Miniute > other.Miniute)
                return 1;
            else if (this.Miniute<other.Miniute)
                return -1;
            else{ return 0;}
        }
    }
}