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
        }
    }
}
