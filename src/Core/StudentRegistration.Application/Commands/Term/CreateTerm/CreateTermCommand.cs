namespace StudentRegistration.Application.Commands;

public class CreateTermCommand : IRequest<int>
{
    public SemesterType? SemesterType { get; init; }
    public int? Year { get; init; }
    public TermWeeklySlotsDTO? TermWeeklySlots { get; init; }
}

