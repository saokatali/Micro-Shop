using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MicroShopMobile.Models;
using Xamarin.AttributeValidation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MicroShopMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private SignInDto model;
        public Login()
        {
            InitializeComponent();
            BindingContext = model= new SignInDto();
        }

        private async void SignIn(object sender, EventArgs e)
        {
            var isValid = await this.ValidateAsync();
            if (isValid)
            {
                var app = Application.Current as App;

                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync(app.Settings.IdentityUrl + "/Account/SignIn", model);
                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    Preferences.Set("token", user.Token);
                    await Navigation.PopAsync();
                    await Navigation.PushAsync(new Dashboard());

                }
                else
                {
                    await DisplayAlert("Error", "Incorrect Email or Password", "Ok");

                }

            }
        }

        private async void SignUp(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await Navigation.PushAsync(new SignUp());
        }
    }
}