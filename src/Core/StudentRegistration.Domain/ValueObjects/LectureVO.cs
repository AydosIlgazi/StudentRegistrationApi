namespace StudentRegistration.Domain.ValueObjects;

public class LectureVO
{
    public string LectureId{get;}
    public List<Classroom> LectureClassSlots{get;}
    public int Capacity{get;}
}