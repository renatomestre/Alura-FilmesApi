using FilmesApi.Data.DTOs;
using FilmesApi.Models;

namespace FilmesApi.Profile
{
    public class CinemaProfile : AutoMapper.Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();

            CreateMap<Cinema, ReadCinemaDto>()
                .ForMember(cinemaDto => cinemaDto.Endereco,
                    opt => opt.MapFrom(cinema => cinema.Endereco))
                .ForMember(cinemaDto => cinemaDto.Sessoes,
                    opt => opt.MapFrom(cinema => cinema.Sessoes));

            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}