namespace StudentRegistration.Domain.UnitTests.Aggregates;

public class TermTest 
{

    public static TheoryData<List<DailySlots>> InvalidTermWeeklySlotsData =>
        new TheoryData<List<DailySlots>>
        {
            new List<DailySlots>{
                new DailySlotsBuilder().WithDay(Day.Monday).Build(),
                new DailySlotsBuilder().WithDay(Day.Monday).Build(),                
            },
            new List<DailySlots>{
                new DailySlotsBuilder().WithDay(Day.Monday).Build(),
                new DailySlotsBuilder().WithDay(Day.Tuesday).Build(),
                new DailySlotsBuilder().WithDay(Day.Wednesday).Build(),              
                new DailySlotsBuilder().WithDay(Day.Saturday).Build(),              
                new DailySlotsBuilder().WithDay(Day.Wednesday).Build()              
            }
        };

    public static TheoryData<List<DailySlots>> ValidTermWeeklySlotsData =>
        new TheoryData<List<DailySlots>>
        {
            new List<DailySlots>{
                new DailySlotsBuilder().WithDay(Day.Monday).Build(),
            },
            new List<DailySlots>{
                new DailySlotsBuilder().WithDay(Day.Monday).Build(),
                new DailySlotsBuilder().WithDay(Day.Wednesday).Build(),
                new DailySlotsBuilder().WithDay(Day.Friday).Build(),
            },
            new List<DailySlots>{
                new DailySlotsBuilder().WithDay(Day.Monday).Build(),
                new DailySlotsBuilder().WithDay(Day.Tuesday).Build(),
                new DailySlotsBuilder().WithDay(Day.Wednesday).Build(), 
                new DailySlotsBuilder().WithDay(Day.Thursday).Build(), 
                new DailySlotsBuilder().WithDay(Day.Friday).Build(), 
                new DailySlotsBuilder().WithDay(Day.Saturday).Build(),
                new DailySlotsBuilder().WithDay(Day.Sunday).Build(), 
            }
        };
    
    [Theory]
    [MemberData(nameof(ValidTermWeeklySlotsData))]
    public void create_term_weekly_slots_successfully(List<DailySlots> dailySlots)
    {
        //Act
        var termWeeklySlots = new TermWeeklySlots(dailySlots);
        //Assert
        Assert.NotNull(termWeeklySlots);
    }

    [Theory]
    [MemberData(nameof(InvalidTermWeeklySlotsData))]
    public void invalid_term_weekly_slots_creation_same_day_occurs_more_than_once(List<DailySlots> dailySlots)
    {
        //Act-Assert
        Assert.Throws<Exception>(()=>new TermWeeklySlots(dailySlots));
    }

    [Fact]
    public void term_status_is_waiting_and_enrollment_is_not_active_when_term_created()
    {
        //Act
        Term newTerm = new TermBuilder().Build();

        //Assert
        Assert.Equal(TermStatus.Waiting, newTerm.Status);
        Assert.False(newTerm.IsEnrollmentActive);
    }

    [Fact]
    public void start_term_sets_term_status_and_throws_term_started_event()
    {
        //Arrange
        Term newTerm = new TermBuilder().Build();

        //Act
        newTerm.StartTerm();

        //Assert
        Assert.Equal(TermStatus.Active,newTerm.Status);
        Assert.Contains(newTerm.DomainEvents,@event =>
        {
            if(@event is TermStartedDomainEvent termStartedDomainEvent){
                if(termStartedDomainEvent.LectureDays == newTerm.LectureDaysAndSlots)
                {
                    return true;
                }
            }
            return false;
        });
    }

    [Fact]
    public void starting_active_term_throws_exception()
    {
        // Arrange
        Term term = new TermBuilder().Build();
        term.StartTerm();
    
        // Act && Assert
        Assert.Throws<Exception>(()=>term.StartTerm());    
    }

    [Fact]
    public void end_term_sets_term_status_and_throws_term_ended_event()
    {
        //Arrange
        Term term = new TermBuilder().Build();
        term.StartTerm();

        //Act
        term.EndTerm();

        //Assert
        Assert.Equal(TermStatus.Completed,term.Status);
        Assert.Contains(term.DomainEvents,@event =>
        {
            if(@event is TermEndedDomainEvent termEndedDomainEvent){
                if(termEndedDomainEvent.TermId == term.Id)
                {
                    return true;
                }
            }
            return false;
        });
    }

    [Fact]
    public void ending_completed_term_throws_exception()
    {
        // Arrange
        Term term = new TermBuilder().Build();
        term.StartTerm();
        term.EndTerm();
    
        // Act && Assert
        Assert.Throws<Exception>(()=>term.EndTerm());    
    }

    [Fact]
    public void open_enrollment_successfully_when_term_is_active_and_enrollment_is_not_active()
    {
        // Arrange
        Term term = new TermBuilder().Build();
        term.StartTerm();

        //Act
        term.OpenEnrollment();

        //Assert
        Assert.True(term.IsEnrollmentActive); 
    }

    [Fact]
    public void open_enrollment_throws_exception_when_term_status_is_waiting()
    {
        //Arrange
        Term term = new TermBuilder().Build();
    
        // Act && Assert
        Assert.Throws<Exception>(()=>term.OpenEnrollment());

    }
    [Fact]
    public void open_enrollment_throws_exception_when_term_status_is_completed()
    {
        //Arrange
        Term term = new TermBuilder().Build();
        term.EndTerm();
    
        // Act && Assert
        Assert.Throws<Exception>(()=>term.OpenEnrollment());

    }

    [Fact]
    public void open_enrollment_throws_exception_when_enrollment_has_already_opened()
    {
        //Arrange
        Term term = new TermBuilder().Build();
        term.StartTerm();
        term.OpenEnrollment();
    
        // Act && Assert
        Assert.Throws<Exception>(()=>term.OpenEnrollment());

    }


    [Fact]
    public void close_enrollment_successfully_when_term_is_active_and_enrollment_has_opened()
    {
        // Arrange
        Term term = new TermBuilder().Build();
        term.StartTerm();
        term.OpenEnrollment();

        //Act
        term.CloseEnrollment();

        //Assert
        Assert.False(term.IsEnrollmentActive); 
    }

    [Fact]
    public void close_enrollment_throws_exception_when_term_status_is_waiting()
    {
        //Arrange
        Term term = new TermBuilder().Build();
    
        // Act && Assert
        Assert.Throws<Exception>(()=>term.OpenEnrollment());

    }

    [Fact]
    public void close_enrollment_throws_exception_when_term_status_is_completed()
    {
        //Arrange
        Term term = new TermBuilder().Build();
        term.EndTerm();
    
        // Act && Assert
        Assert.Throws<Exception>(()=>term.CloseEnrollment());

    }

    [Fact]
    public void close_enrollment_throws_exception_when_enrollment_has_already_closed()
    {
        //Arrange
        Term term = new TermBuilder().Build();
        term.StartTerm();
        term.OpenEnrollment();
        term.CloseEnrollment();
    
        // Act && Assert
        Assert.Throws<Exception>(()=>term.CloseEnrollment());

    }


}