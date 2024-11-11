using AutoMapper;
using myNotesService.Domain;
using myNotesService.Model;

namespace myNotesService.Mapper
{
    public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Source, Destination>
        CreateMap<GeneralNotesDTO, GeneralNotes>();
        CreateMap<GeneralNotes, GeneralNotesDTO>();
    }
}
}