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
            CreateMap<EditFIlmRequestModel, Film>().ForMember(x => x.Categories, opt => opt.Ignore()); 
            CreateMap<Category, CategoryModel>();
            CreateMap<Category, EditCategoryModel>().ReverseMap();         
        }
    }
}
