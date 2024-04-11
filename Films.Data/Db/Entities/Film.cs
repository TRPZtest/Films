using System.ComponentModel.DataAnnotations;

namespace Films.Data.Db.Entities
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        [Length(1, 200)]
        public string Name { get; set; }
        [Length(1, 200)]
        public string Director {  get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Category> Categories { get; set; } = [];
    }
}
