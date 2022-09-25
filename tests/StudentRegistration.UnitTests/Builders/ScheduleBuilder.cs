namespace StudentRegistration.UnitTests.Builders;

public class ScheduleBuilder
{
    private TermVO _term;

    public ScheduleBuilder()
    {
        var term = new TermBuilder().Build();
        _term = new TermVO {
            LectureDaysAndSlots = term.LectureDaysAndSlots,
            TermId = term.Id
        };
    }
    public Schedule Build()
    {
        return new Schedule(_term);
    }
}