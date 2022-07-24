namespace StudentRegistration.Domain.UnitTests.Builders;

public class SectionBuilder
{
    private string _clasroomId;
    private List<DaySlot> _daySlots;
    private bool _isDefault=true;
    private List<DaySlot> _overridenDaySlots;

    public SectionBuilder()
    {
        _daySlots = new List<DaySlot>{
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(10,30).WithEndTime(11,30).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Tuesday).WithSlot(
                    new SlotBuilder().WithStartTime(11,30).WithEndTime(12,30).Build()).Build(),
        };
        _clasroomId = "1";
        _overridenDaySlots = new List<DaySlot>();
    }

    public SectionBuilder WithClassroomId(string id)
    {
        _clasroomId = id;
        return this;
    }
    public SectionBuilder WithDaySlot(DaySlot daySlot)
    {
        _isDefault = false;
        _overridenDaySlots.Add(daySlot);
        return this;
    }
    public Section Build()
    {
        if(_isDefault)
        {
            return new Section(_clasroomId, _daySlots);
        }
        return new Section(_clasroomId, _overridenDaySlots);
        
    }
}