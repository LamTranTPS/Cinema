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
    public static class EventExtension
    {
        public static EventViewModel ToViewModel(this Event self)
        {
            return Mapper.Map<EventViewModel>(self);
        }

        public static IEnumerable<EventViewModel> ToViewModel(this IEnumerable<Event> self)
        {
            return Mapper.Map<IEnumerable<EventViewModel>>(self);
        }

        public static Event ToEntityModel(this EventViewModel self)
        {
            return Mapper.Map<Event>(self);
        }

        public static IEnumerable<Event> ToEntityModel(this IEnumerable<EventViewModel> self)
        {
            return Mapper.Map<IEnumerable<Event>>(self);
        }
    }
}