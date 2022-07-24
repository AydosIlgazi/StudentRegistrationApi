namespace StudentRegistration.Domain.UnitTests.Builders;

public class DaySlotBuilder
{
    private Day _day;
    private Slot _slot;
    private bool _isAvailable;

    private List<Slot> _slotList;

    public DaySlotBuilder()
    {
        _day = Day.Monday;
        _slot=new SlotBuilder().Build();
        _slotList = new List<Slot>()
        {
            new SlotBuilder().WithStartTime(9,0).WithEndTime(10,0).Build(),
            new SlotBuilder().WithStartTime(9,45).WithEndTime(10,30).Build(),
            new SlotBuilder().WithStartTime(10,35).WithEndTime(10,55).Build(),
            new SlotBuilder().WithStartTime(13,13).WithEndTime(14,01).Build(),
            new SlotBuilder().WithStartTime(14,15).WithEndTime(15,0).Build(),
        };
        _isAvailable = true;
    }

    public DaySlotBuilder WithDay(Day day)
    {
        _day = day;
        return this;
    }

    public DaySlotBuilder WithSlot(Slot slot)
    {
        _slot = slot;
        return this;
    }
    public DaySlotBuilder WithSlot(int slotId)
    {
        _slot = _slotList[slotId];
        return this;
    }
    public DaySlotBuilder WithIsAvailable(bool IsAvailable)
    {
        _isAvailable = IsAvailable;
        return this;
    }

    public DaySlot Build()
    {
        return new DaySlot{Day=_day,Slot=_slot,IsAvailable =_isAvailable};
    }
}