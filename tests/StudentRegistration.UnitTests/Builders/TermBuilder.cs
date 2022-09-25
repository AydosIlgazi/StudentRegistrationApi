namespace StudentRegistration.UnitTests.Builders;

public class TermBuilder
{
    private Semester _semester;
	private TermWeeklySlots _lectureDaysAndSlots;
    private TermStatus _termStatus;
    private int _id;

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
        _id = 0;

    }
    public TermBuilder WithSemester(SemesterType semesterType, int year)
    {
        _semester = new Semester
        {
            SemesterType = semesterType,
            Year = year
        };
        return this;
    }
    public TermBuilder WithId(int id)
    {
        _id = id;
        return this;
    }
    public Term Build()
    {
        Term term = new Term(_semester, _lectureDaysAndSlots);
        term.Id = _id;
        return term;
    }
}