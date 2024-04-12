using Films.Data.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Films.Data.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryFilm> CategoriesFilms { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>()
               .HasMany(e => e.Categories)
               .WithMany(e => e.Films)
               .UsingEntity<CategoryFilm>();

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Drama" },
                new Category { Id = 2, Name = "Comedy" },
                new Category { Id = 3, Name = "Action" },
                new Category { Id = 4, Name = "Science Fiction" },
                new Category { Id = 5, Name = "Horror", ParentCategoryId = 3 },
                new Category { Id = 6, Name = "Adventure", ParentCategoryId = 3 },
                new Category { Id = 7, Name = "Documentary" }
            );
          
            modelBuilder.Entity<Film>().HasData(
                new Film { Id = 1, Name = "The Shawshank Redemption", Director = "Frank Darabont", ReleaseDate = new DateTime(1994, 10, 14) },
                new Film { Id = 2, Name = "The Godfather", Director = "Francis Ford Coppola", ReleaseDate = new DateTime(1972, 3, 24) },
                new Film { Id = 3, Name = "The Dark Knight", Director = "Christopher Nolan", ReleaseDate = new DateTime(2008, 7, 18) },
                new Film { Id = 4, Name = "The Lord of the Rings: The Return of the King", Director = "Peter Jackson", ReleaseDate = new DateTime(2003, 12, 17) },
                new Film { Id = 5, Name = "Pulp Fiction", Director = "Quentin Tarantino", ReleaseDate = new DateTime(1994, 10, 14) },
                new Film { Id = 6, Name = "Fight Club", Director = "David Fincher", ReleaseDate = new DateTime(1999, 10, 15) },
                new Film { Id = 7, Name = "Forrest Gump", Director = "Robert Zemeckis", ReleaseDate = new DateTime(1994, 7, 6) },
                new Film { Id = 8, Name = "Inception", Director = "Christopher Nolan", ReleaseDate = new DateTime(2010, 7, 16) },
                new Film { Id = 9, Name = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski", ReleaseDate = new DateTime(1999, 3, 31) },
                new Film { Id = 10, Name = "The Silence of the Lambs", Director = "Jonathan Demme", ReleaseDate = new DateTime(1991, 2, 14) },
                new Film { Id = 11, Name = "The Social Network", Director = "David Fincher", ReleaseDate = new DateTime(2010, 10, 1) },
                new Film { Id = 12, Name = "Gladiator", Director = "Ridley Scott", ReleaseDate = new DateTime(2000, 5, 5) },
                new Film { Id = 13, Name = "The Green Mile", Director = "Frank Darabont", ReleaseDate = new DateTime(1999, 12, 10) },
                new Film { Id = 14, Name = "The Departed", Director = "Martin Scorsese", ReleaseDate = new DateTime(2006, 10, 6) },
                new Film { Id = 15, Name = "Back to the Future", Director = "Robert Zemeckis", ReleaseDate = new DateTime(1985, 7, 3) }
            );

            modelBuilder.Entity<CategoryFilm>().HasData(
                new CategoryFilm { FilmId = 1, CategoryId = 1 },
                new CategoryFilm { FilmId = 1, CategoryId = 3 },
                new CategoryFilm { FilmId = 2, CategoryId = 1 },
                new CategoryFilm { FilmId = 3, CategoryId = 3 },
                new CategoryFilm { FilmId = 4, CategoryId = 6 },
                new CategoryFilm { FilmId = 4, CategoryId = 4 },
                new CategoryFilm { FilmId = 5, CategoryId = 1 }     
            );
        }
    }
}
