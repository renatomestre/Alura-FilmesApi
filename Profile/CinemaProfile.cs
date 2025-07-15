using FilmesApi.Data.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profile
{
    public class CinemaProfile : AutoMapper.Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<Cinema, ReadCinemaDto>();
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}