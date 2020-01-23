using Newtonsoft.Json;
using PromotionFinderConsole.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PromotionFinderConsole
{
    public class PromotionFinder
    {
        private readonly string _baseUrl;
        // TODO:
        // Declare private field for HttpClient
        private readonly HttpClient _httpClient;

        public PromotionFinder(string baseUrl)
        {
            _baseUrl = baseUrl;
            // TODO:
            // Create HttpClient and set BaseAddress
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<IEnumerable<string>> GetCheapProducts()
        {
            // TODO: 
            // Get products and deserialize to IEnumerable<ProductModel> (JsonConvert) using a continuation flow
            var products = Get("products").ContinueWith(t => JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(t.Result));

            // TODO:
            // Get promotions and deserialize to IEnumerable<PromotionModel> (JsonConvert) using a continuation flow
            var promotions = Get("promotions").ContinueWith(t => JsonConvert.DeserializeObject<IEnumerable<PromotionModel>>(t.Result));

            // TODO:
            // Await both asynchronous jobs
            return await Task.WhenAll(products, promotions).ContinueWith(tasks =>
           {
               return products.Result.SelectMany(product =>
               {
                   var promotionsForThisProduct = promotions.Result.Where(promotion => product.CategoryId == promotion.CategoryId);
                   return promotionsForThisProduct.Select(promotion =>
                   {
                       return $"[{promotion.Name}] {product.Name} is now at {promotion.DiscountPercentage}% discount]";
                   });
               });
           });
        }

        // TODO (bonus):
        // Add a CancellationToken as 2nd argument to stop the loop as soon as a cancellation is requested
        public void PollForCheapProducts(int intervalMilliseconds, CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    var cheapProducts = await GetCheapProducts();
                    foreach (var cheapProduct in cheapProducts)
                    {
                        Console.WriteLine(cheapProduct);
                    }
                    Thread.Sleep(intervalMilliseconds);
                }
            });
        }

        private async Task<string> Get(string url)
        {
            // TODO:
            // Get the string result from the HttpClient with the given url
            return await _httpClient.GetStringAsync(url);
        }
    }
}
