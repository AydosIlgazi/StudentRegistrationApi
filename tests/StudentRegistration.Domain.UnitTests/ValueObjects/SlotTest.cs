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
        Assert.Throws<Exception>(() => new Slot(start, end));
         
    }
}
