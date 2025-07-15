using FilmesApi.Data.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profile
{
    public class SessaoProfile : AutoMapper.Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>();
        }
    }
}