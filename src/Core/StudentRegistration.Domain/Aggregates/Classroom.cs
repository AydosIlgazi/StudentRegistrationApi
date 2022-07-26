namespace StudentRegistration.Domain.Aggregates;

public class Classroom : AggregateRoot
{
    private int _termId;
    private string _classroomId;
    private List<DaySlot> _classSlots;
    public IReadOnlyCollection<DaySlot> ClassSlots=>_classSlots;
    public Classroom(string classroomId, TermVO term)
    {
        _classroomId = classroomId;
        _termId = term.TermId;
        _classSlots = new List<DaySlot>();
        foreach(DailySlots ds in term.LectureDaysAndSlots.DailySlots){
            foreach(Slot s in ds.Slots){
                _classSlots.Add(new DaySlot{
                    Day = ds.Day,
                    Slot = s
                });
            }
        }
    }

    public void ReserveSlotForLecture(List<DaySlot> daySlots)
    {
        foreach(DaySlot ds in daySlots)
        {
            var _daySlot =_classSlots.Where(cs=> cs.Day == ds.Day && cs.Slot == ds.Slot).FirstOrDefault();
            if(_daySlot == null){
                throw new Exception("This day and slot is not usable in this term");
            }
            if(_daySlot.IsAvailable==false){
                throw new Exception("Slot in this classroom is already reserved");
            }
            DaySlot reservedSlot = new DaySlot{
                Day = ds.Day,
                Slot = ds.Slot,
                IsAvailable = false,
            };
            var index = _classSlots.IndexOf(_daySlot);

            if (index != -1)
                _classSlots[index] = reservedSlot;

        }

    }
}