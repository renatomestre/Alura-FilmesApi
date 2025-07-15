using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.DTOs;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController(FilmeContext context, IMapper mapper) : ControllerBase
{
    private FilmeContext Context => context;
    private IMapper Mapper => mapper;

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = Mapper.Map<Filme>(filmeDto);

        Context.Filmes.Add(filme);
        Context.SaveChanges();

        return CreatedAtAction(
            nameof(RecuperaFilmePorId),
            new { id = filme.Id },
            filme
        );
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        Filme? filme = Context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();

        Mapper.Map(filmeDto, filme);
        Context.SaveChanges();

        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        Filme? filme = Context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();

        return Ok(filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return Context.Filmes.Skip(skip).Take(take);
    }
}