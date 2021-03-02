using MicroShopMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MicroShopMobile.Services
{
    public static class UrlHelper
    {
        public static string Get<T>()
        {
           

            switch (typeof(T).Name)
            {
                case "SignInDto":
                    return  "";

                default:
                    return string.Empty;
            }

        }

        public static string GetAll<T>()
        {


            switch (typeof(T).Name)
            {
                case "SignInDto":
                    return "";

                default:
                    return string.Empty;
            }

        }

        public static string Post<T>()
        {


            switch (typeof(T).Name)
            {
                case "Product":
                    return "Catalog";

                default:
                    return string.Empty;
            }

        }
    }
}
