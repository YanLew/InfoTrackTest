using AutoMapper;
using InfoTrackTest.Models.Entities;

namespace InfoTrackTest.Models.SearchEngines
{
    public class SearchEngineMapper : Profile
    {
        public SearchEngineMapper()
        {
            CreateMap<SearchEngine, SearchEngineOptionDto>();
        }
    }
}
