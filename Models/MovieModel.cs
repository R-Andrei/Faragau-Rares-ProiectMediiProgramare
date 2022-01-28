using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediiProgramareEntity.Models
{
    [Table("Movies")]
    public class MovieModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int MovieId { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Peak { get; set; }
        public Int64 WorldBoxOffice { get; set; }
        public int Year { get; set; }

        public int GenreId { get; set; }
        public GenreModel Genre { get; set; }

        public int StudioId { get; set; }
        public StudioModel Studio { get;set; }

        public MovieModel()
        {
            MovieId = 0;
            Name = string.Empty;
            Rank = 0;
            Peak = 0;
            WorldBoxOffice = 0;
            Year = 0;
            GenreId = 0;
            StudioId = 0;
            Genre = new GenreModel();
            Studio = new StudioModel();
        }

        public MovieModel(
            int id, string name, int rank, 
            int peak, long worldBoxOffice, int year,
            int genreId, int studioId,
            GenreModel genre, StudioModel studio
        )
        {
            MovieId = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Rank = rank;
            Peak = peak;
            WorldBoxOffice = worldBoxOffice;
            Year = year;
            GenreId = genreId;
            StudioId = studioId;
            Genre = genre ?? throw new ArgumentNullException(nameof(genre));
            Studio = studio ?? throw new ArgumentNullException(nameof(studio));
        }
    }
}
