using AutoMapper;
using PrimerApi.Dto;
using PrimerApi.Models;
using PrimerApi.Query;

namespace PrimerApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioDto>();
        CreateMap<Avion, AvionDto>();
        CreateMap<Avion, nuevoAvion>();
        CreateMap<nuevoAvion, Avion>(); 
        CreateMap<MarcaAvion, MarcaAvionDto>();
        CreateMap<MarcaAvionDto, MarcaAvionDto>();
    }

}