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
            var app = Application.Current as App;
            var baseUrl = app.Settings.ApiUrl;

            switch (typeof(T).Name)
            {
                case "SignInDto":
                    return baseUrl + "";

                default:
                    return string.Empty;
            }

        }
    }
}
