using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;


namespace MicroShopMobile
{
    class MicroShopHttpClientHandler:HttpClientHandler
    {
        public MicroShopHttpClientHandler()
        {
            this.ServerCertificateCustomValidationCallback += (sender, cert, chaun, ssPolicyError) =>
            {
                return true;
            };
        }

    }
}
