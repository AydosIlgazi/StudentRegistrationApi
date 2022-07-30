namespace StudentRegistration.Domain.UnitTests.Aggregates;

public class LecturerTests
{
    private const string lecturerId = "1234567";
    private const int termId=12;

    [Fact]
    public void create_new_lecturer_with_term()
    {
        //Arrange
        TermVO term =  new TermVO{
                TermId = termId,
                LectureDaysAndSlots = new TermBuilder().Build().LectureDaysAndSlots
        };
        Schedule lecturerSchedule = new Schedule(term);

        //Act
        Lecturer lecturer = new Lecturer(lecturerId, lecturerSchedule);

        //Assert
        Assert.NotNull(lecturer);
    }

    [Fact]
    public void adding_same_term_multiple_times_to_lecture_is_invalid()
    {
        //Arrange
        TermVO term =  new TermVO{
                TermId = termId,
                LectureDaysAndSlots = new TermBuilder().Build().LectureDaysAndSlots
        };
        Schedule lecturerSchedule = new Schedule(term);
        Lecturer lecturer = new Lecturer(lecturerId,lecturerSchedule);

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(()=>lecturer.AddNewScheduleToLecturer(lecturerSchedule));
    }
}