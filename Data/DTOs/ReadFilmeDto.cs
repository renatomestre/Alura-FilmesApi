namespace FilmesApi.Data.DTOs
{
    public class ReadFilmeDto
    {
        public int Duracao { get; set; }

        public string Genero { get; set; }

        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;

        public ICollection<ReadSessaoDto> Sessoes { get; set; }

        public string Titulo { get; set; }
    }
}