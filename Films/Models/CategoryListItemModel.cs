using System.ComponentModel.DataAnnotations;

namespace Films.Models
{
    public class CategoryListItemModel
    {
        public int Id { get; set; }
        [Length(1, 200)]
        public string Name { get; set; }
        public int NestingLevel { get; set; }       
        public int FilmsCount { get; set; }
    }
}
