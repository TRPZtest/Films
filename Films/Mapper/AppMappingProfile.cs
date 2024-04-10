using AutoMapper;
using Films.Data.Db.Entities;
using Films.Models;
using System;

namespace Films.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Film, FilmModel>();
            CreateMap<Film, EditFilmModel>();
            CreateMap<Category, CategoryModel>();
        }
    }
}
