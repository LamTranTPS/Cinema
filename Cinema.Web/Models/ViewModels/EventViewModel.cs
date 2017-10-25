using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.ViewModels
{
    public class EventViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public string Time { set; get; }
        public DateTime? EndTime { set; get; }
        public string LinkImage { set; get; }
        public string Intro { set; get; }
        public string CinemaChainID { set; get; }
        public string CinemaChainName { set; get; }
    }
}