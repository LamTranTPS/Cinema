using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models.ViewModels
{
    public class CinemaViewModel
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public string LinkImage { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
        public string Intro { set; get; }
        public decimal? Longitude { set; get; }
        public decimal? Latitude { set; get; }
        public string LocationID { set; get; }
        public string CinemaChainID { set; get; }
        public string LocationName { set; get; }
        public string CinemaChainName { set; get; }
    }
}