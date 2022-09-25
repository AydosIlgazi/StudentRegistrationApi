namespace StudentRegistration.UnitTests.Builders;

public class SlotBuilder
{
    private SlotTime _startTime;
    private SlotTime _endTime;

    public SlotBuilder(){
        _startTime = new SlotTime{Hour=9, Miniute=0};
        _endTime = new SlotTime{Hour=10,Miniute=0};
    }
    public SlotBuilder WithStartTime(int hour, int minute)
    {
        _startTime = new SlotTime{Hour=hour, Miniute=minute};
        return this;
    }

    public SlotBuilder WithEndTime(int hour, int minute)
    {
        _endTime = new SlotTime{Hour=hour, Miniute=minute};
        return this;
    }

    public Slot Build()
    {
        return new Slot(_startTime,_endTime);
    }
}

