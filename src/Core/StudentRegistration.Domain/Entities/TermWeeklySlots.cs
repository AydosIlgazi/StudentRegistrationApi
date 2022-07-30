namespace StudentRegistration.Domain.Entities;

public class TermWeeklySlots : BaseEntity
{
    private List<DailySlots> _dailySlots;
    public IReadOnlyCollection<DailySlots> DailySlots => _dailySlots;

    public TermWeeklySlots(List<DailySlots> dailySlots)
    {
        var duplicateElements = dailySlots.GroupBy(x => x.Day)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();
        if(duplicateElements.Count>0)
        {
            throw new StudentRegistrationDomainException("Duplicate day");
        }
        _dailySlots = dailySlots;
    }
}