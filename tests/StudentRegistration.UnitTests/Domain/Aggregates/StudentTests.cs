namespace StudentRegistration.UnitTests.Domain.Aggregates;

public class StudentTests
{
    private readonly TermVO _term;
    private readonly LectureVO _lecture;
    private const string _studentId = "218257";

    public StudentTests()
    {
        _term = new TermVO()
        {
            TermId = 1,
            LectureDaysAndSlots = new TermBuilder().Build().LectureDaysAndSlots,
            IsEnrollmentActive = true,
        };
        _lecture = new LectureVO()
        {
            LectureId = 342432,
            LectureSections = new List<Section>() {
                new SectionBuilder().WithClassroomId("1").Build(),
                new SectionBuilder().WithClassroomId("2").WithDaySlot(new DaySlotBuilder().WithDay(Day.Friday).WithSlot(5).Build()).Build(),
            },
            Capacity = 75
        };
    }

    public static TheoryData<Slot, Day, bool> ScheduleAvailableData =>
    new TheoryData<Slot, Day, bool>
    {
            { new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build(), Day.Monday, true},
            { new SlotBuilder().WithStartTime(10,30).WithEndTime(11,30).Build(), Day.Monday, false},
            { new SlotBuilder().WithStartTime(13,0).WithEndTime(13,45).Build(), Day.Wednesday, false},
            { new SlotBuilder().WithStartTime(14,30).WithEndTime(15,15).Build(), Day.Wednesday, true},

    };

    public static TheoryData<Slot, Day> InvalidScheduleAvailableData =>
    new TheoryData<Slot, Day>
    {
            { new SlotBuilder().WithStartTime(9,45).WithEndTime(10,30).Build(), Day.Monday},
            { new SlotBuilder().WithStartTime(10,30).WithEndTime(11,29).Build(), Day.Monday},
            { new SlotBuilder().WithStartTime(13,0).WithEndTime(14,0).Build(), Day.Wednesday},
            { new SlotBuilder().WithStartTime(14,45).WithEndTime(15,15).Build(), Day.Wednesday},

    };


    [Fact]
    public void create_schedule_successfully()
    {

        //Arrange && Act
        Schedule schedule = new Schedule(_term);

        //Assert
        Assert.NotNull(schedule);
    }

    [Fact]
    public void fill_schedule_successfully()
    {
        //Arrange
        Schedule schedule = new Schedule(_term);
        DaySlot filledDaySlot1 = new DaySlotBuilder().WithSlot
            (new SlotBuilder().WithStartTime(9, 30).WithEndTime(10, 30).Build()).WithDay(Day.Monday).Build();
        DaySlot filledDaySlot2 = new DaySlotBuilder().WithSlot
            (new SlotBuilder().WithStartTime(14, 30).WithEndTime(15, 15).Build()).WithDay(Day.Wednesday).Build();

        DaySlot daySlot1 = new DaySlotBuilder().WithSlot
            (new SlotBuilder().WithStartTime(9, 30).WithEndTime(10, 30).Build()).WithDay(Day.Tuesday).Build();
        DaySlot daySlot2 = new DaySlotBuilder().WithSlot
            (new SlotBuilder().WithStartTime(13, 0).WithEndTime(13, 45).Build()).WithDay(Day.Wednesday).Build();

        //Act
        schedule.FillTheSchedule(new List<DaySlot>
        {
            filledDaySlot1,
            filledDaySlot2
        });

        //Assert
        Assert.False(schedule.ScheduleSlots.Where(ds => ds == filledDaySlot1).First().IsAvailable);
        Assert.False(schedule.ScheduleSlots.Where(ds => ds == filledDaySlot2).First().IsAvailable);

        Assert.True(schedule.ScheduleSlots.Where(ds => ds == daySlot1).First().IsAvailable);
        Assert.True(schedule.ScheduleSlots.Where(ds => ds == daySlot2).First().IsAvailable);
    }

    [Fact]
    public void end_schedule()
    {
        //Arrange
        var schedule = new ScheduleBuilder().Build();

        //Act
        schedule.EndSchedule();

        //Assert
        Assert.False(schedule.IsActive);
    }

    [Theory]
    [MemberData(nameof(ScheduleAvailableData))]
    public void schedule_is_available_successfully(Slot slot, Day day, bool isAvailable)
    {
        //Arrange
        Schedule schedule = new Schedule(_term);
        schedule.FillTheSchedule(new List<DaySlot>
        {
            new DaySlotBuilder().WithSlot(new SlotBuilder().WithStartTime(10,30).WithEndTime(11,30).Build()).WithDay(Day.Monday).Build(),
            new DaySlotBuilder().WithSlot(new SlotBuilder().WithStartTime(13,0).WithEndTime(13,45).Build()).WithDay(Day.Wednesday).Build()
        });

        //Act
        bool _isAvailable = schedule.IsScheduleAvailable(slot, day);

        //Assert
        Assert.Equal(isAvailable, _isAvailable);

    }

    [Theory]
    [MemberData(nameof(InvalidScheduleAvailableData))]
    public void schedule_is_available_invalid_when_slots_are_not_available_in_term(Slot slot, Day day)
    {
        //Arrange
        Schedule schedule = new Schedule(_term);

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => schedule.IsScheduleAvailable(slot, day));

    }

    [Fact]
    public void create_enrollment_successfully()
    {
        //Act
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);

        //Assert
        Assert.NotNull(enrollment);
        Assert.Equal(EnrollmentStatus.Active, enrollment.EnrollmentStatus);
        Assert.Equal(_lecture.LectureId, enrollment.LectureId);
    }

    [Fact]
    public void creating_enrollment_is_invalid_when_enrollment_not_active_in_term()
    {
        //Arrange
        TermVO term = new TermVO()
        {
            IsEnrollmentActive = false,
        };

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => new Enrollment(_lecture, term, 10));
    }

    [Theory]
    [InlineData(75)]
    [InlineData(100)]
    [InlineData(500)]
    public void creating_enrollment_is_invalid_when_lecture_is_full(int enrollmentCount)
    {

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => new Enrollment(_lecture, _term, enrollmentCount));
    }
    [Fact]
    public void approve_enrollment_successfully()
    {
        //Arrange
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);

        //Act
        enrollment.ApproveEnrollment();

        //Assert
        Assert.Equal(EnrollmentStatus.Completed, enrollment.EnrollmentStatus);
    }

    [Fact]
    public void cancel_enrollment_successfully()
    {
        //Arrange
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);

        //Act
        enrollment.CancelEnrollment();

        //Assert
        Assert.Equal(EnrollmentStatus.Left, enrollment.EnrollmentStatus);
    }

    [Fact]
    public void approving_enrollment_is_invalid_when_enrollment_already_approved()
    {
        //Arrange
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);
        enrollment.ApproveEnrollment();

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => enrollment.ApproveEnrollment());
    }

    [Fact]
    public void approving_enrollment_is_invalid_when_enrollment_canceled()
    {
        //Arrange
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);
        enrollment.CancelEnrollment();

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => enrollment.ApproveEnrollment());
    }

    [Fact]
    public void canceling_enrollment_is_invalid_when_enrollment_already_canceled()
    {
        //Arrange
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);
        enrollment.CancelEnrollment();

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => enrollment.CancelEnrollment());
    }

    [Fact]
    public void canceling_enrollment_is_invalid_when_enrollment_approved()
    {
        //Arrange
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);
        enrollment.ApproveEnrollment();

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(() => enrollment.CancelEnrollment());
    }

    [Fact]
    public void create_student()
    {
        //Act
        Student student = new Student(_studentId, new ScheduleBuilder().Build());

        //Assert
        Assert.NotNull(student);
    }


    [Fact]
    public void enroll_to_lecture()
    {
        //Arrange
        Student student = new Student(_studentId, new ScheduleBuilder().Build());
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);

        //Act
        student.EnrollToLecture(_lecture, enrollment);

        //Assert
        Assert.Contains(enrollment, student.Enrollments);
        Assert.Contains(student.DomainEvents, @event =>
        {
            if (@event is StudentEnrollmentCreatedDomainEvent studentEnrollmentCreatedDomainEvent)
            {
                if (studentEnrollmentCreatedDomainEvent.StudentId == _studentId && studentEnrollmentCreatedDomainEvent.LectureId==_lecture.LectureId)
                {
                    return true;
                }
            }
            return false;
        });

        var lectureSlots = _lecture.LectureSections.SelectMany(lectureSections => lectureSections.SectionSlots);


        var scheduleDaySlots = student.Schedules.Where(schedule => schedule.IsActive == true)
            .SelectMany(schedule => schedule.ScheduleSlots)
            .Where(daySlot => lectureSlots.Any(ls => ls.Day == daySlot.Day && ls.Slot == daySlot.Slot));
        
        Assert.Equal(4,scheduleDaySlots.Count());

        foreach (var daySlot in scheduleDaySlots)
        {
            Assert.False(daySlot.IsAvailable);
        };
    }

    [Fact]
    public void enrolling_lecture_is_invalid_when_student_have_active_enrollment_that_lecture()
    {
        //Arrange
        Student student = new Student(_studentId, new ScheduleBuilder().Build());
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);
        student.EnrollToLecture(_lecture, enrollment);


        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(()=>student.EnrollToLecture(_lecture,enrollment));
    }

    [Fact]
    public void enrolling_lecture_is_invalid_when_student_not_have_active_schedule()
    {
        //Arrange
        var schedule = new ScheduleBuilder().Build();
        schedule.EndSchedule();
        Student student = new Student(_studentId, schedule);
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);

        //Act && Assert
        Assert.Throws<StudentRegistrationDomainException>(()=> student.EnrollToLecture(_lecture, enrollment));
    }

    [Fact]
    public void enrollment_is_invalid_when_student_schedule_is_not_available_for_dayslot()
    {
        //Arrange
        Student student = new Student(_studentId, new ScheduleBuilder().Build());
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);
        student.EnrollToLecture(_lecture, enrollment);
        //Second lecture with intercepting slot lecture1
        LectureVO lecture2 = new LectureVO()
        {
            LectureId = 342432,
            LectureSections = new List<Section>() {
                new SectionBuilder().WithClassroomId("3").WithDaySlot(new DaySlotBuilder().WithDay(Day.Friday).WithSlot(5).Build()).Build(),
            },
            Capacity = 20
        };
        Enrollment enrollment2 = new Enrollment(lecture2, _term, 10);

        //Act && Assert

        Assert.Throws<StudentRegistrationDomainException>(()=> student.EnrollToLecture(lecture2, enrollment2));
    }

    [Fact]
    public void remove_enrollment()
    {
        //Arrange
        Student student = new Student(_studentId, new ScheduleBuilder().Build());
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);
        student.EnrollToLecture(_lecture, enrollment);

        //Act
        student.RemoveEnrollment(_lecture.LectureId);

        //Assert
        Assert.Equal(EnrollmentStatus.Left, student.Enrollments.Where(e => e.LectureId ==_lecture.LectureId).Select(e=>e.EnrollmentStatus).First());
        Assert.Contains(student.DomainEvents, @event =>
        {
            if (@event is StudentEnrollmentCanceledDomainEvent studentEnrollmentCanceledDomainEvent)
            {
                if (studentEnrollmentCanceledDomainEvent.StudentId == _studentId && studentEnrollmentCanceledDomainEvent.LectureId == _lecture.LectureId)
                {
                    return true;
                }
            }
            return false;
        });
    }
    [Fact]
    public void removing_enrollment_is_invalid_when_student_not_enrolled()
    {
        //Arrange
        Student student = new Student(_studentId, new ScheduleBuilder().Build());
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);

        //Act && Assert

        Assert.Throws<StudentRegistrationDomainException>(() => student.RemoveEnrollment(1));
    }

    [Fact]
    public void removing_enrollment_is_invalid_when_student_already_left_enrollment()
    {
        //Arrange
        Student student = new Student(_studentId, new ScheduleBuilder().Build());
        Enrollment enrollment = new Enrollment(_lecture, _term, 10);
        student.EnrollToLecture(_lecture,enrollment);
        student.RemoveEnrollment(_lecture.LectureId);

        //Act && Assert

        Assert.Throws<StudentRegistrationDomainException>(() => student.RemoveEnrollment(_lecture.LectureId));
    }

}

