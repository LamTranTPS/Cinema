using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.ViewModels
{
    public class ScheduleViewModel
    {
        public string ID { set; get; }
        public string LinkTicket { set; get; }
        public DateTime DateTime { set; get; }
        public string Type { set; get; }
        public int CinemaID { set; get; }
        public int FilmID { set; get; }
        public int CinemaName { set; get; }
        public int FilmName { set; get; }
    }
}