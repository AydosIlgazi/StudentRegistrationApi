namespace StudentRegistration.Domain.Entities;

public class Classroom : BaseEntity{
    private string _classroomId;
    private List<DaySlot> _classSlots;
    public IReadOnlyCollection<DaySlot> ClassSlots=>_classSlots;
    public Classroom(string classroomId, TermVO term, List<DaySlot> daySlots =null)
    {
        _classroomId = classroomId;
        _classSlots = new List<DaySlot>();
        foreach(DailySlots ds in term.LectureDaysAndSlots.DailySlots){
            foreach(Slot s in ds.Slots){
                _classSlots.Add(new DaySlot{
                    Day = ds.Day,
                    Slot = s
                });
            }
        }
        foreach(DaySlot ds in daySlots)
        {
            ReserveSlotForLecture(ds.Slot, ds.Day);
        }
    }

    public void ReserveSlotForLecture(Slot slot, Day day)
    {
        var _daySlot =_classSlots.Where(cs=> cs.Day == day && cs.Slot == slot).FirstOrDefault();
        if(_daySlot == null){
            throw new Exception("This day and slot is not usable in this term");
        }
        if(_daySlot.IsAvailable==false){
            throw new Exception("Slot in this classroom is already reserved");
        }
        DaySlot reservedSlot = new DaySlot{
            Day = day,
            Slot = slot,
            IsAvailable = false,
        };
        _daySlot = reservedSlot;

    }
    
}