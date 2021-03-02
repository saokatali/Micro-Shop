using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroShopMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.AttributeValidation;
using System.Net.Http;
using System.Net.Http.Json;
using Xamarin.Essentials;

namespace MicroShopMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        private SignUpDto model;

        public SignUp()
        {

            InitializeComponent();
            BindingContext = model = new SignUpDto();

        }


        
        private async void Save(object sender, EventArgs e)
        {
            var isValid = await this.ValidateAsync();
            if (isValid)
            {
                var app = Application.Current as App;

                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync(app.Settings.IdentityUrl + "/Account/SignUp", model);
                if(response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    Preferences.Set("token",user.Token);
                    await Navigation.PopAsync();
                    await Navigation.PushAsync(new Dashboard());

                }
                else
                {
                    await DisplayAlert("Error", "Something went wrrong. Please try again", "Ok");
                    
                }
               
             



            } 


        }
    }
}