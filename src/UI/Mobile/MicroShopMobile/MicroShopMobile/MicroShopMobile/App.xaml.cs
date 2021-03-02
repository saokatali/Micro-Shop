using MicroShopMobile.Views;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.Json;
using Xamarin.Essentials;

namespace MicroShopMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LoadAppSettings();
            if (string.IsNullOrEmpty(Preferences.Get("token", string.Empty)))
            {
                MainPage = new NavigationPage(new Login());
            }
            else
            {
                MainPage = new NavigationPage(new Dashboard());
            }
        }

        public AppSettings Settings { get; private set; }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void LoadAppSettings()
        {
#if RELEASE
            var appSettingsStream = Assembly.GetAssembly(typeof(AppSettings)).GetManifestResourceStream("MicroShopMobile.appsettings.release.json");
   
#else
            var appSettingsStream = Assembly.GetAssembly(typeof(AppSettings)).GetManifestResourceStream("MicroShopMobile.appsettings.debug.json");
#endif
            using (var reader = new StreamReader(appSettingsStream))
            {
                string settings = reader.ReadToEnd();
                Settings = JsonSerializer.Deserialize<AppSettings>(settings, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }




        }
    }
}
