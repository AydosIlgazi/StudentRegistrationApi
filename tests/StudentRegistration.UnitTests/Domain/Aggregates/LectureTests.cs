namespace StudentRegistration.UnitTests.Domain.Aggregates;

public class LectureTests
{
    private const string classroomId = "Classroom 5";
    private const string courseId = "3";
    private const int lectureCapacity=80;
    private const string lecturerId = "123455";

    private readonly TermVO activeTerm;
    private readonly CourseDTO courseWithMaxWeeklyMins;
    private readonly ClassroomDTO classroomWithMaxCapacity;

    public LectureTests()
    {
        activeTerm =new TermVO{
            TermStatus = TermStatus.Active
        };
        courseWithMaxWeeklyMins = new CourseDTO{
            CourseId = courseId,
            TotalMinutesWeekly = int.MaxValue
        };
        classroomWithMaxCapacity = new ClassroomDTO{
            Capacity = int.MaxValue
        };
    }
    public static TheoryData<List<DaySlot>> ValidSectionDaySlots =>
        new TheoryData<List<DaySlot>>
        {
            new List<DaySlot>{
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(10,30).WithEndTime(11,30).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(11,30).WithEndTime(12,30).Build()).Build(),
            },
            new List<DaySlot>{
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Tuesday).WithSlot(
                    new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Friday).WithSlot(
                    new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build()).Build(),
            },

        };
    public static TheoryData<List<DaySlot>> InValidSectionDaySlots =>
        new TheoryData<List<DaySlot>>
        {
            new List<DaySlot>{
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(10,30).WithEndTime(11,30).Build()).Build(),
            },
            new List<DaySlot>{
                new DaySlotBuilder().WithDay(Day.Monday).WithSlot(
                    new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Friday).WithSlot(
                    new SlotBuilder().WithStartTime(15,0).WithEndTime(16,0).Build()).Build(),
                new DaySlotBuilder().WithDay(Day.Friday).WithSlot(
                    new SlotBuilder().WithStartTime(15,0).WithEndTime(16,0).Build()).Build(),
            },

        };
    
    public static TheoryData<Section,int> SectionDuration =>
        new TheoryData<Section, int>
        {
            { new SectionBuilder().Build(), 180},
            {new SectionBuilder()
            .WithDaySlot(new DaySlotBuilder().WithDay(Day.Monday).WithSlot(0).Build())
            .WithDaySlot(new DaySlotBuilder().WithDay(Day.Tuesday).WithSlot(1).Build())
            .Build(), 105},
            {new SectionBuilder()
            .WithDaySlot(new DaySlotBuilder().WithDay(Day.Sunday).WithSlot(0).Build())
            .WithDaySlot(new DaySlotBuilder().WithDay(Day.Friday).WithSlot(2).Build())
            .WithDaySlot(new DaySlotBuilder().WithDay(Day.Sunday).WithSlot(3).Build())
            .WithDaySlot(new DaySlotBuilder().WithDay(Day.Wednesday).WithSlot(4).Build())
            .Build(), 173}
        };

    [Theory]
    [MemberData(nameof(ValidSectionDaySlots))]
    public void create_section_successfully(List<DaySlot> sectionDaySlots)
    {
        // Arrange && Act
        Section section = new Section(classroomId,sectionDaySlots);
    
        // Assert
        Assert.NotNull(section);
    }

    [Theory]
    [MemberData(nameof(InValidSectionDaySlots))]
    public void create_section_throws_exception_when_duplicate_slots(List<DaySlot> sectionDaySlots)
    {
        // Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => new Section(classroomId,sectionDaySlots)) ;
    
    }

    [Fact]
    public void create_new_lecture_successfully()
    {
    
        // Act
        Lecture lecture = new Lecture(activeTerm,courseWithMaxWeeklyMins,lectureCapacity);

        // Assert
        Assert.NotNull(lecture);
    }

    [Theory]
    [InlineData(TermStatus.Waiting)]
    [InlineData(TermStatus.Completed)]
    public void creating_lecture_is_not_allowed_when_term_is_not_active(TermStatus termStatus)
    {
        // Arrange
        TermVO term = new TermVO{
            TermStatus = termStatus
        };
    
        // Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(()=> new Lecture(term,courseWithMaxWeeklyMins,lectureCapacity));
    }

    [Fact]
    public void add_section_to_lecture()
    {
        //Arrange
        Section section = new SectionBuilder().Build();
        Lecture lecture = new Lecture(activeTerm,courseWithMaxWeeklyMins,lectureCapacity);

        //Act
        lecture.AddSectionToLecture(section,classroomWithMaxCapacity);

        //Assert
        Assert.Contains(section,lecture.LectureSections);
    }
    [Theory]
    [InlineData(-4)]
    [InlineData(0)]
    [InlineData(100)]
    [InlineData(179)]
    public void lecture_total_duration_cannot_exceed_courses_weekly_duration(int totalMinutesWeekly)
    {
        //Arrange
        CourseDTO course = new CourseDTO{
            CourseId = courseId,
            TotalMinutesWeekly = totalMinutesWeekly
        };
        Section section = new SectionBuilder().Build();
        Lecture lecture = new Lecture(activeTerm,course,lectureCapacity);

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(()=>lecture.AddSectionToLecture(section,classroomWithMaxCapacity));
    }

    [Theory]
    [InlineData(-20)]
    [InlineData(0)]
    [InlineData(79)]
    public void section_capacity_cannot_exceed_classrooms_capacity(int classroomCapacity)
    {
        //Arrange
        ClassroomDTO classroom = new ClassroomDTO{
            Capacity = classroomCapacity
        };
        Section section = new SectionBuilder().Build();
        Lecture lecture = new Lecture(activeTerm,courseWithMaxWeeklyMins,lectureCapacity);

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(()=>lecture.AddSectionToLecture(section,classroom));
    }

    [Theory]
    [MemberData(nameof(SectionDuration))]
    public void calculate_duration_for_given_section(Section section, int duration)
    {
        //Arrange
        Lecture lecture = new Lecture(activeTerm,courseWithMaxWeeklyMins,lectureCapacity);
        lecture.AddSectionToLecture(section,classroomWithMaxCapacity);

        //Act
        int _duration = lecture.GetTotalLectureDuration();

        //Assert
        Assert.Equal(_duration,duration);
    }

    [Fact]
    public void assign_lecture_to_lecturer_successfully()
    {

        //Arrange 
        LecturerVO lecturer = new LecturerVO(){
            LecturerId = lecturerId,
            Schedule = new ScheduleBuilder().Build()
        };

        CourseDTO course = new CourseDTO{
            CourseId = courseId,
            TotalMinutesWeekly = 180
        };

        Lecture lecture = new Lecture(activeTerm,course,lectureCapacity);
        Section section = new SectionBuilder().Build();
        lecture.AddSectionToLecture(section,classroomWithMaxCapacity);

        //Act
        lecture.AssignLecturerToLecture(lecturer);

        //Assert
        Assert.Equal(lecture.LecturerId,lecturerId);
        Assert.Contains(lecture.DomainEvents,@event =>
        {
            if(@event is LecturerAssignedToLectureDomainEvent lecturerAssignedToLectureDomainEvent){
                if(lecturerAssignedToLectureDomainEvent.LecturerId == lecturer.LecturerId)
                {
                    return true;
                }
            }
            return false;
        });
    }


    [Theory]
    [InlineData(181)]
    [InlineData(200)]
    [InlineData(500)]
    public void lecturer_cannot_be_assigned_when_lecture_duration_not_equal_course_duration(int totalMinutesWeekly)
    {
        //Arrange
        CourseDTO course = new CourseDTO{
            CourseId = courseId,
            TotalMinutesWeekly = totalMinutesWeekly
        };

         LecturerVO lecturer = new LecturerVO(){
            LecturerId = lecturerId,
            Schedule = new ScheduleBuilder().Build()
        };
        Lecture lecture = new Lecture(activeTerm,course,lectureCapacity);
        Section section = new SectionBuilder().Build();
        lecture.AddSectionToLecture(section,classroomWithMaxCapacity);

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(()=>lecture.AssignLecturerToLecture(lecturer));

    }

}