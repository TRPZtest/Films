using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Data.Db.Entities
{
    [PrimaryKey(nameof(CategoryId), nameof(FilmId))]
    public class CategoryFilm
    {
        [ForeignKey(nameof(Category))]
        public int CategoryId {  get; set; }
        [ForeignKey(nameof(Film))]
        public int FilmId {  get; set; }
        public Category Category { get; set; }
        public Film Film { get; set; }
    }
}
