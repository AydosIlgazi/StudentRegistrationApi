namespace StudentRegistration.Domain.ValueObjects;

public class LectureVO
{
    public string LectureId{get;}
    public List<Section> LectureSections{get;}
    public int Capacity{get;}
}