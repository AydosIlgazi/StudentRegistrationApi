namespace StudentRegistration.Domain.Entities;

public class Schedule : BaseEntity
{
    private string _termId;
    private List<DaySlot> _scheduleSlots;
    private bool _isActive;
    public string TermId =>_termId;
    public bool IsActive => _isActive;
    public Schedule(TermVO term)
    {
        _scheduleSlots = new List<DaySlot>();
        _isActive = true;
        _termId = term.TermId;
        foreach(TermDailySlots ds in term.DailySlots){
            foreach(Slot s in ds.Slots){
                _scheduleSlots.Add(new DaySlot{
                    Day = ds.Day,
                    Slot = s,
                    IsAvailable = true
                });
            }
        }
    }
    public bool IsScheduleAvailable(Slot slot, Day day)
    {
        var _daySlot =_scheduleSlots.Where(ss=>ss.Day ==day && ss.Slot==slot).FirstOrDefault();
        if(_daySlot == null){
            throw new Exception("This day and slot is not usable in this term");
        }
        return _daySlot.IsAvailable;
         
    }
    public void FillTheSchedule(IReadOnlyCollection<DaySlot> daySlots)
    {
        foreach(DaySlot ds in daySlots)
        {
            var _daySlot =_scheduleSlots.Where(ss=>ss.Day ==ds.Day && ss.Slot==ds.Slot).FirstOrDefault();
            _daySlot = new DaySlot{
                Day=ds.Day,
                Slot=ds.Slot,
                IsAvailable = false
            };
        }
    }
}