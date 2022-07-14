namespace StudentRegistration.Domain.ValueObjects;

public class DailySlotsTests 
{
    public static TheoryData<List<Slot>> ValidDailySlotsData=>
        new TheoryData<List<Slot>>
        {
                new List<Slot>{ 
                    new Slot(new SlotTime{Hour=9, Miniute=45}, new SlotTime{Hour=10, Miniute=45}),
                    new Slot(new SlotTime{Hour=10, Miniute=45}, new SlotTime{Hour=11, Miniute=45}),
                    new Slot(new SlotTime{Hour=8, Miniute=45}, new SlotTime{Hour=9, Miniute=45})
                    },
                new List<Slot>{ 
                    new Slot(new SlotTime{Hour=15, Miniute=30}, new SlotTime{Hour=16, Miniute=15}),
                    new Slot(new SlotTime{Hour=9, Miniute=0}, new SlotTime{Hour=9, Miniute=45}),
                    new Slot(new SlotTime{Hour=10, Miniute=45}, new SlotTime{Hour=11, Miniute=30})
                    },
            
        };

    public static TheoryData<List<Slot>> InvalidDailySlotsData=>
        new TheoryData<List<Slot>>
        {
                new List<Slot>{ 
                    new Slot(new SlotTime{Hour=9, Miniute=0}, new SlotTime{Hour=9, Miniute=45}),
                    new Slot(new SlotTime{Hour=9, Miniute=44}, new SlotTime{Hour=10, Miniute=30}),
                    new Slot(new SlotTime{Hour=10, Miniute=30}, new SlotTime{Hour=11, Miniute=15})
                    },
                new List<Slot>{ 
                    new Slot(new SlotTime{Hour=9, Miniute=30}, new SlotTime{Hour=10, Miniute=30}),
                    new Slot(new SlotTime{Hour=10, Miniute=45}, new SlotTime{Hour=11, Miniute=45}),
                    new Slot(new SlotTime{Hour=8, Miniute=45}, new SlotTime{Hour=9, Miniute=45})
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
        //assert
        Assert.Throws<Exception>(() =>new DailySlots(slots));
    }
}