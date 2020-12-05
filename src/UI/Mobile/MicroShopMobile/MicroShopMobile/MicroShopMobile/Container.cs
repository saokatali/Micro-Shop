﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace MicroShopMobile
{
    public static class Container
    {
        public static IServiceProvider Services { get; private set; }
        public static void RegisterServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<HttpClientHandler, MicroShopHttpClientHandler>();
            serviceCollection.AddSingleton<HttpClient, MicroShopHttpClient>();

            Services = serviceCollection.BuildServiceProvider();
        }
    }
}
