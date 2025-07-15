using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.DTOs;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController(FilmeContext context, IMapper mapper) : ControllerBase
{
    private FilmeContext Context => context;
    private IMapper Mapper => mapper;

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
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

    /// <summary>
    /// Atualiza um filme no banco de dados usando seu id
    /// </summary>
    /// <param name="id">Id do filme a ser atualizado no banco</param>
    /// <param name="filmeDto">Objeto com os campos necessários para atualização de um filme</param>
    /// <returns>Sem conteúdo de retorno</returns>
    /// <response code="204">Caso o id seja existente na base de dados e o filme tenha sido atualizado</response>
    /// <response code="404">Caso o id seja inexistente na base de dados</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        Filme? filme = Context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();

        Mapper.Map(filmeDto, filme);
        Context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza apenas um atributo do filme no banco de dados usando seu id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patch"></param>
    /// <returns>Sem conteúdo de retorno</returns>
    /// <response code="204">Caso o id seja existente na base de dados e o filme tenha sido atualizado</response>
    /// <response code="404">Caso o id seja inexistente na base de dados</response>
    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        Filme? filme = Context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();

        UpdateFilmeDto? filmeParaAtualizar = Mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        Mapper.Map(filmeParaAtualizar, filme);
        Context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Deleta um filme do banco de dados usando seu id
    /// </summary>
    /// <param name="id">Id do filme a ser removido do banco</param>
    /// <returns>Sem conteúdo de retorno</returns>
    /// <response code="204">Caso o id seja existente na base de dados e o filme tenha sido removido</response>
    /// <response code="404">Caso o id seja inexistente na base de dados</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaFilme(int id)
    {
        Filme? filme = Context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();

        Context.Remove(filme);
        Context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Recupera um filme no banco de dados usando seu id
    /// </summary>
    /// <param name="id">Id do filme a ser recuperado no banco</param>
    /// <returns>Informações do filme buscado</returns>
    /// <response code="200">Caso o id seja existente na base de dados</response>
    /// <response code="404">Caso o id seja inexistente na base de dados</response>
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        Filme? filme = Context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();

        return Ok(filme);
    }

    /// <summary>
    /// Recupera uma lista de filmes do banco de dados
    /// </summary>
    /// <param name="skip">Número de filmes que serão pulados</param>
    /// <param name="take">Número de filmes que serão recuperados</param>
    /// <returns>Informações dos filmes buscados</returns>
    /// <response code="200">Com a lista de filmes presentes na base de dados</response>
    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return Context.Filmes.Skip(skip).Take(take);
    }
}