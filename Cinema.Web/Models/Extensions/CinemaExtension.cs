using AutoMapper;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using Cinema.Web.Models.ViewModels;
using System.Collections.Generic;

namespace Cinema.Web.Models.Extensions
{
    public static class CinemaExtension
    {
        public static CinemaViewModel ToViewModel(this Model.Models.Cinema self)
        {
            return Mapper.Map<CinemaViewModel>(self);
        }

        public static IEnumerable<CinemaViewModel> ToViewModel(this IEnumerable<Model.Models.Cinema> self)
        {
            return Mapper.Map<IEnumerable<CinemaViewModel>>(self);
        }

        public static Model.Models.Cinema ToEntityModel(this CinemaViewModel self)
        {
            return Mapper.Map<Model.Models.Cinema>(self);
        }

        public static IEnumerable<Model.Models.Cinema> ToEntityModel(this IEnumerable<CinemaViewModel> self)
        {
            return Mapper.Map<IEnumerable<Model.Models.Cinema>>(self);
        }
    }
}