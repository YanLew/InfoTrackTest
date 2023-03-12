using AutoMapper;
using InfoTrackTest.Models.Entities;

namespace InfoTrackTest.Models.Histories
{
    public class HistoryMapper : Profile
    {
        public HistoryMapper()
        {
            CreateMap<History, HistoryDto>()
                .ForMember(
                    dest => dest.SearchEngineName,
                    opt => opt.MapFrom(src => src.SearchEngine.Name)
                );
        }
    }
}
