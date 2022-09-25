namespace StudentRegistration.Application.DTOs;

public record DailySlotsDTO
{
    public Day Day { get; init; }
    public List<SlotDTO> Slots { get; init; }
}
