
namespace StudentRegistration.Application.Mappings;

public class GeneralMapping:Profile
{
    public GeneralMapping()
    {
        CreateMap<TermWeeklySlotsDTO,TermWeeklySlots>(MemberList.Source).ReverseMap();
        CreateMap<DailySlotsDTO, DailySlots>().ReverseMap();
        CreateMap<SlotDTO,Slot>().ReverseMap();
        CreateMap<SlotTimeDTO,SlotTime>().ReverseMap();

    }
}
