namespace StudentRegistration.Domain.Entities;

public class Section : BaseEntity{
    private string _classroomId;
    private List<DaySlot> _sectionSlots;
    public IReadOnlyCollection<DaySlot> SectionSlots=>_sectionSlots;
    public Section(string classroomId, List<DaySlot> sectionSlots)
    {
        var duplicates = sectionSlots.GroupBy(i => new {i.Day, i.Slot})
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if(duplicates.Count>0){
            throw new StudentRegistrationDomainException("Duplicate slots");
        }
        _classroomId = classroomId;
        _sectionSlots = sectionSlots;

    }

    
}