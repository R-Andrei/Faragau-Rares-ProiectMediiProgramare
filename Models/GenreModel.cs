using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace MediiProgramareEntity.Models
{
    [Table("Genres")]
    public class GenreModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<MovieModel> ?Movies { get; set; }

        public GenreModel()
        {
            GenreId = -1;
            Name = string.Empty;
            Description = string.Empty;
        }
        public GenreModel(int id, string name, string description
            )
        {
            GenreId = id;
            Name = name;
            Description = description;
        }
    }
}
