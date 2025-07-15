using FilmesApi.Data.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profile
{
    public class FilmeProfile : AutoMapper.Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme, UpdateFilmeDto>();
            CreateMap<Filme, ReadFilmeDto>()
                .ForMember(filmeDto => filmeDto.Sessoes,
                    opt => opt.MapFrom(filme => filme.Sessoes)
                );
        }
    }
}