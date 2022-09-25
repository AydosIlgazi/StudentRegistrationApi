
using AutoMapper;

namespace StudentRegistration.UnitTests.Application.Mapping;

public class GeneralMappingTests
{
    public static TheoryData<SlotTimeDTO, SlotTimeDTO> SlotDTOData =>
        new TheoryData<SlotTimeDTO, SlotTimeDTO>
        {
            {
                new SlotTimeDTO{Hour=8,Miniute=30},
                new SlotTimeDTO{Hour=9,Miniute=15}
            },
            {
                new SlotTimeDTO{Hour=15,Miniute=43},
                new SlotTimeDTO{Hour=16,Miniute=25}
            },
            {
                new SlotTimeDTO{Hour=10,Miniute=0},
                new SlotTimeDTO{Hour=11,Miniute=0}
            }

        };
    public static TheoryData<Day, List<SlotDTO>> DailySlotsDTOData =>
    new TheoryData<Day, List<SlotDTO>>
    {
            {
                Day.Monday,
                new List<SlotDTO>{ 
                    new SlotDTO {
                        StartTime=new SlotTimeDTO {Hour=9,Miniute=0 },
                        EndTime=new SlotTimeDTO {Hour=10,Miniute=0}
                    } ,

                }
            },
            {
                Day.Friday,
                new List<SlotDTO>{
                    new SlotDTO {
                        StartTime=new SlotTimeDTO {Hour=11,Miniute=55 },
                        EndTime=new SlotTimeDTO {Hour=12,Miniute=7}
                    } ,
                    new SlotDTO {
                        StartTime=new SlotTimeDTO {Hour=13,Miniute=15 },
                        EndTime=new SlotTimeDTO {Hour=14,Miniute=51}
                    } ,
                    new SlotDTO {
                        StartTime=new SlotTimeDTO {Hour=17,Miniute=40 },
                        EndTime=new SlotTimeDTO {Hour=19,Miniute=25}
                    } ,

                }
            }
    };


    public static TheoryData<List<DailySlotsDTO>> TermWeeklySlotsDTOData =>
    new TheoryData<List<DailySlotsDTO>>
    {
            new List<DailySlotsDTO>
            {
                new DailySlotsDTO
                {
                    Day = Day.Monday,
                    Slots = new List<SlotDTO>
                    {
                        new SlotDTO {
                            StartTime=new SlotTimeDTO {Hour=11,Miniute=55 },
                            EndTime=new SlotTimeDTO {Hour=12,Miniute=7}
                        } ,
                        new SlotDTO {
                            StartTime=new SlotTimeDTO {Hour=13,Miniute=15 },
                            EndTime=new SlotTimeDTO {Hour=14,Miniute=51}
                        } ,
                        new SlotDTO {
                            StartTime=new SlotTimeDTO {Hour=17,Miniute=40 },
                            EndTime=new SlotTimeDTO {Hour=19,Miniute=25}
                        } ,
                    }
                    
                },
                new DailySlotsDTO
                {
                    Day=Day.Tuesday,
                    Slots= new List<SlotDTO>{
                        new SlotDTO {
                            StartTime=new SlotTimeDTO {Hour=9,Miniute=0 },
                            EndTime=new SlotTimeDTO {Hour=10,Miniute=0}
                        } ,

                    }
                }
            }

    };

    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>());
        var invalidConfigException = Record.Exception(() => config.AssertConfigurationIsValid());
        Assert.Null(invalidConfigException);
    }

    [Theory]
    [InlineData(8, 30)]
    [InlineData(10, 50)]
    [InlineData(17, 8)]
    public void SlotTimeDto_To_SlotTime_Mapping_IsValid(int hour, int minute)
    {
        //Arrange
        SlotTimeDTO slotTimeDTO = new SlotTimeDTO { Hour = hour, Miniute = minute };
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>());
        var mapper = config.CreateMapper();

        //Act
        var slotTime = mapper.Map<SlotTime>(slotTimeDTO);

        //Assert
        Assert.Equal(hour, slotTime.Hour);
        Assert.Equal(minute, slotTime.Miniute);
    }

    [Theory]
    [MemberData(nameof(SlotDTOData))]
    public void SlotDTO_To_Slot_Mapping_IsValid(SlotTimeDTO startDto, SlotTimeDTO endDto)
    {
        //Arrange
        SlotDTO slotDTO = new SlotDTO { StartTime = startDto, EndTime = endDto };
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>());
        var mapper = config.CreateMapper();

        //Act
        var slot = mapper.Map<Slot>(slotDTO);

        //Assert
        Assert.Equal(startDto.Hour, slot.StartTime.Hour);
        Assert.Equal(startDto.Miniute, slot.StartTime.Miniute);
        Assert.Equal(endDto.Hour, slot.EndTime.Hour);
        Assert.Equal(endDto.Miniute, slot.EndTime.Miniute);
    }

    [Theory]
    [MemberData(nameof(DailySlotsDTOData))]
    public void DailySlotsDTO_To_DailySlots_Mapping_IsValid(Day day, List<SlotDTO> slots)
    {
        //Arrange
        DailySlotsDTO dailySlotsDTO = new DailySlotsDTO { Day = day, Slots=slots };
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>());
        var mapper = config.CreateMapper();

        //Act
        var dailySlots = mapper.Map<DailySlots>(dailySlotsDTO);

        //Assert
        Assert.Equal(dailySlots.Day, day);
        Assert.Equal(slots.Count, dailySlotsDTO.Slots.Count);
        int i=0;
        foreach(Slot slot in dailySlots.Slots)
        {
            Assert.Equal(slot.StartTime.Hour, slots[i].StartTime.Hour);
            Assert.Equal(slot.StartTime.Miniute, slots[i].StartTime.Miniute);
            Assert.Equal(slot.EndTime.Hour, slots[i].EndTime.Hour);
            Assert.Equal(slot.EndTime.Miniute, slots[i].EndTime.Miniute);
            i++;
        }    

    }

    [Theory]
    [MemberData(nameof(TermWeeklySlotsDTOData))]
    public void TermWeeklySlotsDTO_To_TermWeeklySlots_Mapping_IsValid(List<DailySlotsDTO> dailySlots)
    {
        //Arrange
        TermWeeklySlotsDTO termWeeklySlotsDTO = new TermWeeklySlotsDTO { DailySlots= dailySlots };
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>());
        var mapper = config.CreateMapper();

        //Act
        var termWeeklySlots = mapper.Map<TermWeeklySlots>(termWeeklySlotsDTO);

        //Assert
        Assert.Equal(termWeeklySlots.DailySlots.Count, termWeeklySlotsDTO.DailySlots.Count);
        int i = 0;
        foreach (DailySlots ds in termWeeklySlots.DailySlots)
        {
            Assert.Equal(dailySlots[i].Day, ds.Day);
            Assert.Equal(dailySlots[i].Slots.Count, ds.Slots.Count);
            i++;
        }

    }
}
