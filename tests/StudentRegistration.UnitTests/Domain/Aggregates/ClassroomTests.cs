namespace StudentRegistration.UnitTests.Domain.Aggregates;

public class ClassroomTests
{
    private const int termId = 4;
    private const string classroomId = "15";
    private readonly TermVO term;

    
    public ClassroomTests()
    {
        term = new TermVO
        {
            TermId = termId,
            LectureDaysAndSlots = new TermBuilder().Build().LectureDaysAndSlots,
        };
    }

    [Fact]
    public void create_new_classroom_successfully()
    {

        //Arrange && Act
        Classroom classroom = new Classroom(classroomId, term);

        //Assert
        Assert.NotNull(classroom);
    }

    [Fact]
    public void reserve_slot_for_lecture_in_class_successfully()
    {
        //Arrange
        Classroom classroom = new Classroom(classroomId, term);
        Slot reservedSlot = new SlotBuilder().WithStartTime(9, 30).WithEndTime(10, 30).Build();
        DaySlot reservedDaySlot = new DaySlotBuilder().WithDay(Day.Monday).WithSlot(reservedSlot).Build();
        List<DaySlot> reservedSlots = new List<DaySlot> { reservedDaySlot };

        //Act
        classroom.ReserveSlotForLecture(reservedSlots);

        //Assert
        Assert.False(classroom.ClassSlots.Where(ds => ds == reservedDaySlot).First().IsAvailable);

    }

    [Fact]
    public void reserve_slot_for_lecture_is_invalid_when_slot_is_not_available_in_term()
    {
        //Arrange
        Classroom classroom = new Classroom(classroomId, term);
        Slot reservedSlot = new SlotBuilder().WithStartTime(9, 0).WithEndTime(10, 0).Build();
        DaySlot reservedDaySlot = new DaySlotBuilder().WithDay(Day.Monday).WithSlot(reservedSlot).Build();
        List<DaySlot> reservedSlots = new List<DaySlot> { reservedDaySlot };

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => classroom.ReserveSlotForLecture(reservedSlots)); 

    }

    [Fact]
    public void reserve_slot_for_lecture_in_class_is_invalid_when_slot_is_already_reserved()
    { 
        //Arrange
        Classroom classroom = new Classroom(classroomId, term);
        Slot reservedSlot = new SlotBuilder().WithStartTime(9, 30).WithEndTime(10, 30).Build();
        DaySlot reservedDaySlot = new DaySlotBuilder().WithDay(Day.Monday).WithSlot(reservedSlot).Build();
        List<DaySlot> reservedSlots = new List<DaySlot> { reservedDaySlot };
        classroom.ReserveSlotForLecture(reservedSlots);

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => classroom.ReserveSlotForLecture(reservedSlots));

    }


}

