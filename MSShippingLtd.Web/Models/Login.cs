using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSShippingLtd.Web.Models
{
    public class Login
    {

        [Required(ErrorMessage ="Required usernamed", AllowEmptyStrings =false)]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password required", AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}