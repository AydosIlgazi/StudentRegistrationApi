namespace StudentRegistration.Application.DTOs;

public record TermWeeklySlotsDTO
{
    public List<DailySlotsDTO>? DailySlots { get; init; }
}
