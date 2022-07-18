namespace StudentRegistration.Domain.UnitTests.ValueObjects;

public class DailySlotsTests 
{
    public static TheoryData<List<Slot>> ValidDailySlotsData=>
        new TheoryData<List<Slot>>
        {
                new List<Slot>{ 
                    new SlotBuilder().WithStartTime(9,45).WithEndTime(10,45).Build(),
                    new SlotBuilder().WithStartTime(10,45).WithEndTime(11,45).Build(),
                    new SlotBuilder().WithStartTime(8,45).WithEndTime(9,45).Build()
                    },
                new List<Slot>{ 
                    new SlotBuilder().WithStartTime(15,30).WithEndTime(16,15).Build(),
                    new SlotBuilder().WithStartTime(9,0).WithEndTime(9,45).Build(),
                    new SlotBuilder().WithStartTime(10,45).WithEndTime(11,30).Build()
                    },
            
        };

    public static TheoryData<List<Slot>> InvalidDailySlotsData=>
        new TheoryData<List<Slot>>
        {
                new List<Slot>{ 
                    new SlotBuilder().WithStartTime(9,0).WithEndTime(9,45).Build(),
                    new SlotBuilder().WithStartTime(9,44).WithEndTime(10,30).Build(),
                    new SlotBuilder().WithStartTime(10,30).WithEndTime(11,15).Build()
                    },
                new List<Slot>{ 
                    new SlotBuilder().WithStartTime(9,30).WithEndTime(10,30).Build(),
                    new SlotBuilder().WithStartTime(10,45).WithEndTime(11,45).Build(),
                    new SlotBuilder().WithStartTime(8,45).WithEndTime(9,45).Build()
                    }
            
        };


    [Theory]
    [MemberData(nameof(ValidDailySlotsData))]
    public void create_valid_daily_slots(List<Slot> slots)
    {
        //act
        DailySlots dailySlots = new DailySlots(slots);
        
        //assert
        Assert.NotNull(dailySlots);
    }

    [Theory]
    [MemberData(nameof(InvalidDailySlotsData))]
    public void invalid_daily_slots_with_intercepting_slots(List<Slot> slots)
    {
        //act && assert
        Assert.Throws<Exception>(() =>new DailySlots(slots));
    }
}