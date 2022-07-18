namespace StudentRegistration.Domain.UnitTests.Builders;

public class DailySlotsBuilder
{
    private Day _day;
    private List<Slot> _slots;

    public DailySlotsBuilder WithDay(Day day)
    {
        _day = day;
        return this;
    }
    public DailySlotsBuilder()
    {
        _day = Day.Monday;
        _slots = new List<Slot>()
        {
            new SlotBuilder().WithStartTime(9,45).WithEndTime(10,45).Build(),
            new SlotBuilder().WithStartTime(10,45).WithEndTime(11,45).Build(),
            new SlotBuilder().WithStartTime(8,45).WithEndTime(9,45).Build(),
            new SlotBuilder().WithStartTime(13,0).WithEndTime(13,45).Build(),
            new SlotBuilder().WithStartTime(13,45).WithEndTime(14,30).Build(),
            new SlotBuilder().WithStartTime(14,30).WithEndTime(15,15).Build(),

        };
    }
    public DailySlots Build()
    {
        return new DailySlots(_slots){
            Day = _day
        };
    }
}