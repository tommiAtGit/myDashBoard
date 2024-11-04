using AutoMapper;
using myTodoService.Domain;
using myTodoService.Model;

namespace myTodoService.Mapper{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Source, Destination>
        CreateMap<MyTaskDTO, MyTask>();
        CreateMap<MyTask, MyTaskDTO>();
        

    }
}
}
