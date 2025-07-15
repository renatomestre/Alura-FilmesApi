namespace FilmesApi.Data.DTOs
{
    public class ReadCinemaDto
    {
        public ReadEnderecoDto Endereco { get; set; }

        public int Id { get; set; }

        public string Nome { get; set; }

        public ICollection<ReadSessaoDto> Sessoes { get; set; }
    }
}