using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController(FilmeContext context) : ControllerBase
{
    private FilmeContext Context => context;

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        Context.Filmes.Add(filme);
        Context.SaveChanges();

        return CreatedAtAction(
            nameof(RecuperaFilmePorId),
            new { id = filme.Id },
            filme
        );
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