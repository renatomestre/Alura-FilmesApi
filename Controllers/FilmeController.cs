using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    private static readonly List<Filme> filmes = [];
    private static int id = 0;

    [HttpPost]
    public IActionResult AdicionaFilme(
        [FromBody] Filme filme)
    {
        filme.Id = id++;

        filmes.Add(filme);

        return CreatedAtAction(
            nameof(RecuperaFilmePorId),
            new { id = filme.Id },
            filme
        );
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        Filme? filme = filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();

        return Ok(filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return filmes.Skip(skip).Take(take);
    }
}
