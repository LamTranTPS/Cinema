using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Web.Models.ViewModels
{
    public class LocationViewModel
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public decimal? Longitude { set; get; }
        public decimal? Latitude { set; get; }
    }
}