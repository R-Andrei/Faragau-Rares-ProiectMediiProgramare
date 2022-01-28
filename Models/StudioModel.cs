using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediiProgramareEntity.Models
{
    [Table("Studios")]
    public class StudioModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int StudioId { get; set; }
        public string Name { get; set; }
        public int MoviesCreated { get; set; }
        public Int64 HomeBoxOffice { get; set; }
        public Int64 WorldBoxOffice { get; set; }

        public ICollection<MovieModel> ?Movies { get; set; }


        public StudioModel()
        {
            StudioId = -1;
            Name = string.Empty;
            MoviesCreated = 0;
            HomeBoxOffice = 0;
            WorldBoxOffice = 0;
        }
        public StudioModel(
            int id, string name, int movies, long homeBoxOffice, long worldBoxOffice
            )
        {
            StudioId = id;
            Name = name;
            MoviesCreated = movies;
            HomeBoxOffice = homeBoxOffice;
            WorldBoxOffice = worldBoxOffice;
        }
    }
}
