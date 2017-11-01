using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Web.Models
{
    // Models returned by AccountController actions.
    public class ItemViewModel
    {

        public string name { get; set; }
        
        public int quantity { get; set; }
        
        public decimal price { get; set; }
        
        public string currency { get; set; }

        public string RedirectUrl { get; set; }

    }

   
}
