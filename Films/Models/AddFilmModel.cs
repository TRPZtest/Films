using Films.Data.Db.Entities;
using System.ComponentModel.DataAnnotations;

namespace Films.Models
{
    public class AddFilmModel
    {       
        [Length(1, 200)]
        public string Name { get; set; }
        [Length(1, 200)]
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
