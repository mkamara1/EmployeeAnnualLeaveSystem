using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSShippingLtd.Web.Models
{
    public class Registration
    {

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        [Required(ErrorMessage ="email reqired", AllowEmptyStrings =false)]
        public string Email { get; set; }
        [Required(ErrorMessage ="Username required", AllowEmptyStrings =false)]
        public string UserName { get; set; }
        [Required(ErrorMessage ="password required", AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }

    }
}