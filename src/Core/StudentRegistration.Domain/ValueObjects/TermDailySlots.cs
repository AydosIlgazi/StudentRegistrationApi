namespace StudentRegistration.Domain.ValueObjects;
public class TermDailySlots : ValueObject
{
    public Day Day{get; init;} 
    private List<Slot> _slots;
    public IReadOnlyCollection<Slot> Slots => _slots;
    public TermDailySlots(List<Slot> slots)
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
                if(prevSlot.EndTime.CompareTo(nextSlot.StartTime)>1){
                    //slots are intercepting
                }
            }
            prevSlot= nextSlot;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Day;
    }
}