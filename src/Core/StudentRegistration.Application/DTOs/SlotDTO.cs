
namespace StudentRegistration.Application.DTOs;

public record SlotDTO
{
    public string SlotName { get; init; }
    public SlotTimeDTO StartTime { get; init; }
    public SlotTimeDTO EndTime { get; init; }
}
