using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Web.Models
{
    public class ApiResult
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int total { get; set; }
        public object elements { get; set; }

        public ApiResult(bool success, object elements, string message = "", int total = 1)
        {
            this.success = success;
            this.message = message;
            this.total = total;
            this.elements = elements;
        }

        public ApiResult(bool success, object elements, int total, string message = "")
        {
            this.success = success;
            this.message = message;
            this.total = total;
            this.elements = elements;
        }
    }
}