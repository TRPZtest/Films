using Films.Data.Db.Entities;
using System.ComponentModel.DataAnnotations;

namespace Films.Models
{
    public class FilmModel
    {  
        public int Id { get; set; }
        public string Name { get; set; }     
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
