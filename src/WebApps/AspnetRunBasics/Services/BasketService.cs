using System;
using System.Net.Http;

namespace AspnetRunBasics.Services
{
    public class BasketService : IBasketService
    {

        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));


        }
    }
}
