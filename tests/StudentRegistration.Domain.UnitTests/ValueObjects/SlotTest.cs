namespace StudentRegistration.Domain.UnitTests.ValueObjects;

public class SlotTest
{
    public static TheoryData<SlotTime,SlotTime> InvalidSlotTimeData =>
        new TheoryData<SlotTime,SlotTime>
        {
            {
            new SlotTime {Hour=9, Miniute=0},
            new SlotTime {Hour=9, Miniute=0}
            },
            {new SlotTime {Hour=9, Miniute=50},
            new SlotTime {Hour=9, Miniute=30}
            },
            {new SlotTime {Hour=9, Miniute=50},
            new SlotTime {Hour=8, Miniute=55}
            },
        };

    public static TheoryData<Slot,int> SlotDurationData=>
        new TheoryData<Slot, int>
        {
            {
                new SlotBuilder().WithStartTime(9,0).WithEndTime(9,45).Build(),
                45
            },
            {
                new SlotBuilder().WithStartTime(9,0).WithEndTime(10,45).Build(),
                105
            },
            {
                new SlotBuilder().WithStartTime(9,50).WithEndTime(10,25).Build(),
                35
            }
        };

    [Fact]
    public void Create_Slot_Object_Success()
    {
        //Arrange
        SlotTime startTime = new SlotTime{
            Hour = 9,
            Miniute=30
        };

        SlotTime endTime = new SlotTime {
            Hour = 10,
            Miniute=30
        };

        //Act
        Slot slot = new Slot(startTime, endTime);

        //Assert
        Assert.NotNull(slot);
    }

    [Theory]
    [MemberData(nameof(InvalidSlotTimeData))]
    public void Invalid_Slot_Creation_With_EndDate_Is_Earlier_Than_StartDate(SlotTime start, SlotTime end)
    {
        //Act && Assert
        Assert.Throws<Exception>(() => new Slot(start, end));
         
    }

    [Theory]
    [MemberData(nameof(SlotDurationData))]
    public void Slot_Duration_Is_Calculated_Correctly(Slot slot, int duration)
    {

        //Act
        int calculatedDuration = slot.CalculateSlotDuration();

        //Assert
        Assert.Equal(calculatedDuration,duration);
    }
}
