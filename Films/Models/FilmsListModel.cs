namespace Films.Models
{
    public class FilmsListModel
    {
        public IEnumerable<FilmModel> Films { get; set; }
        public FilmsFilterModel FilmsFilter { get; set; }
    }
}
