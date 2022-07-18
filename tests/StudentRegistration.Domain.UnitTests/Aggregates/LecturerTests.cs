namespace StudentRegistration.Domain.UnitTests.Aggregates;

public class LecturerTests
{
    private const string lecturerId = "1234567";
    public static TheoryData<TermVO> TermVO=>
        new TheoryData<TermVO>
        {
            new TermVO{
                TermId = "1",
                LectureDaysAndSlots = new TermBuilder().Build().LectureDaysAndSlots
            }
        };

    [Theory]
    [MemberData(nameof(TermVO))]
    public void create_new_lecturer_with_term(TermVO term)
    {
        //Arrange
        Schedule lecturerSchedule = new Schedule(term);

        //Act
        Lecturer lecturer = new Lecturer(lecturerId, lecturerSchedule);

        //Assert
        Assert.NotNull(lecturer);
    }

    [Theory]
    [MemberData(nameof(TermVO))]
    public void adding_same_term_multiple_times_to_lecture_throws_exception(TermVO term)
    {
        //Arrange
        Schedule lecturerSchedule = new Schedule(term);
        Lecturer lecturer = new Lecturer(lecturerId,lecturerSchedule);

        //Act && Assert
        Assert.Throws<Exception>(()=>lecturer.AddNewScheduleToLecturer(lecturerSchedule));
    }
}