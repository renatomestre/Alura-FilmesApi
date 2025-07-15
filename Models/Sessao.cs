using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Sessao
    {
        public virtual Cinema Cinema { get; set; }

        public int? CinemaId { get; set; }

        public virtual Filme Filme { get; set; }

        [Required]
        public int FilmeId { get; set; }

        [Key]
        [Required]
        public int Id { get; set; }
    }
}