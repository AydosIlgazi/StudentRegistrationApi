namespace StudentRegistration.Domain.ValueObjects;

public class Slot : ValueObject, IComparable<Slot>
{
    public string SlotName {get; init;}
    private SlotTime _startTime;
    private SlotTime _endTime;

    public SlotTime StartTime=>_startTime; 
    public SlotTime EndTime =>_endTime;

    public Slot(SlotTime startTime, SlotTime endTime, int slotDuration=1)
    {

        if(startTime.Hour>endTime.Hour)
        {
            throw new Exception("Slot start time cannot be later than end time");
        }
        if(startTime.Hour == endTime.Hour && startTime.Miniute >= endTime.Miniute)
        {
            throw new Exception("Slot start time cannot be later than end time");
        }
        _startTime = startTime;
        _endTime = endTime;
        int _slotDuration = CalculateSlotDuration();
        if(_slotDuration != slotDuration)
        {
            //ex
        }

    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SlotName;
        yield return StartTime;
        yield return EndTime;
    }
    public int CompareTo(Slot other)
    {
        if(other == null)
            return 1;
        
        if(this.StartTime.Hour > other.StartTime.Hour)
            return 1;
        else if(this.StartTime.Hour< other.StartTime.Hour)
            return -1;
        else {
            if(this.StartTime.Miniute > other.StartTime.Miniute)
                return 1;
            else if (this.StartTime.Miniute<other.StartTime.Miniute)
                return -1;
            else{ return 0;}
        }
    }
    public int CalculateSlotDuration()
    {
        int hourDif = EndTime.Hour-StartTime.Hour;
        int minDiff = EndTime.Miniute -StartTime.Miniute;
        return (hourDif * 60) + minDiff;
    }


    
}
