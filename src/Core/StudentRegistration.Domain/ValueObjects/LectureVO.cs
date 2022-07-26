namespace StudentRegistration.Domain.ValueObjects;

public class LectureVO
{
    public int LectureId { get; init; }
    public List<Section> LectureSections{ get; init; }
    public int Capacity{ get; init; }
}