using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroShopMobile.Models;
using MicroShopMobile.Services;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MicroShopMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadProducts();
        }
        async void LoadProducts()
        {
            var dataService = Container.Services.GetRequiredService<IDataService<Product>>();
            var products = await dataService.GetItemsAsync();
            //  this.BindingContext = products;
            productList.ItemsSource = products;


        }
    }


}