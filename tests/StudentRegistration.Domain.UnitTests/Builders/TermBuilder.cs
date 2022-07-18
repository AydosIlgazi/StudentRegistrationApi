namespace StudentRegistration.Domain.UnitTests.Builders;

public class TermBuilder
{
    private Semester _semester;
	private TermWeeklySlots _lectureDaysAndSlots;

    public TermBuilder()
    {
        _semester = new Semester();
        List<DailySlots> dailySlots = new List<DailySlots>()
        {
            new DailySlotsBuilder().WithDay(Day.Monday).Build(),
            new DailySlotsBuilder().WithDay(Day.Tuesday).Build(),
            new DailySlotsBuilder().WithDay(Day.Wednesday).Build(),
            new DailySlotsBuilder().WithDay(Day.Thursday).Build(),
            new DailySlotsBuilder().WithDay(Day.Friday).Build(),
        };
        _lectureDaysAndSlots = new TermWeeklySlots(dailySlots);
    }

    public Term Build()
    {
        return new Term(_semester,_lectureDaysAndSlots);
    }
}