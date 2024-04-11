using Films.Data.Db.Entities;
using System.ComponentModel.DataAnnotations;

namespace Films.Models
{
    public class EditFIlmRequestModel
    {
        [Required]   
        
        public int Id { get; set; }
        [Length(1, 200)]
        public string Name { get; set; }
        [Length(1, 200)]
        public string Director { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public int[] Categories { get; set; }
    }
}
