using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.AttributeValidation.Attributes;

namespace MicroShopMobile.Models
{
    public class SignInDto
    {
       
        [Required("Please enter User Name")]
        public string UserName { get; set; }
        [Required("Please enter Password")]
        public string Password { get; set; }
    }
}
