using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace MicroShopMobile
{
    public class MicroShopHttpClient:HttpClient
    {
        public MicroShopHttpClient(HttpClientHandler handler):base(handler)
        {
            var app = Application.Current as App;
            BaseAddress = new Uri(app.Settings.ApiUrl);
            DefaultRequestHeaders.Add("Accept", "application/json");           
            DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Preferences.Get("token", string.Empty));

        }

    }
}
