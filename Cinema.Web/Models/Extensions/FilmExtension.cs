using AutoMapper;
using Cinema.Model.Models;
using Cinema.Web.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.Extensions
{
    public static class FilmExtension
    {
        public static FilmViewModel ToViewModel(this Film self)
        {
            return Mapper.Map<FilmViewModel>(self);
        }

        public static IEnumerable<FilmViewModel> ToViewModel(this IEnumerable<Film> self)
        {
            return Mapper.Map<IEnumerable<FilmViewModel>>(self);
        }

        public static Film ToEntityModel(this FilmViewModel self)
        {
            return Mapper.Map<Film>(self);
        }

        public static IEnumerable<Film> ToEntityModel(this IEnumerable<FilmViewModel> self)
        {
            return Mapper.Map<IEnumerable<Film>>(self);
        }
    }
}