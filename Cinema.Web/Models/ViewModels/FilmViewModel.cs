using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.ViewModels
{
    public class FilmViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public DateTime? Premiere { set; get; }
        public string Time { set; get; }
        public string Genre { set; get; }
        public string LinkTrailer { set; get; }
        public string LinkPoster { set; get; }
        public string LinkImage { set; get; }
        public string Intro { set; get; }
        public string Actor { set; get; }
        public string Director { set; get; }
        public string Country { set; get; }
        public string Classification { set; get; }
        public decimal? IMDB { set; get; }
        public bool? IsHot { set; get; }
        public int? ScheduleCount { get; set; }
    }
}