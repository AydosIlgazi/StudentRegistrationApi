namespace StudentRegistration.Domain.ValueObjects;
public class DailySlots : ValueObject
{
    public Day Day{get; init;} 
    private List<Slot> _slots;
    public IReadOnlyCollection<Slot> Slots => _slots;
    public DailySlots(List<Slot> slots)
    {
        slots.Sort();
        Slot prevSlot = default(Slot);
        bool first = true;
        foreach(Slot nextSlot in slots)
        {
            if(first){
                first= false;
            }
            else{
                if(prevSlot.EndTime.CompareTo(nextSlot.StartTime)==1){
                    throw new Exception("Slots are intercepting");
                }
            }
            prevSlot= nextSlot;
        }
        _slots = slots;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Day;
    }
}