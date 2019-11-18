using Newtonsoft.Json;
using PromotionFinderConsole.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public PromotionFinder(string baseUrl)
        {
            _baseUrl = baseUrl;
            // TODO:
            // Create HttpClient and set BaseAddress
        }

        public async Task<IEnumerable<string>> GetCheapProducts()
        {
            // TODO: 
            // Get products and deserialize to IEnumerable<ProductModel> (JsonConvert) using a continuation flow
            var products = Get("products");

            // TODO:
            // Get promotions and deserialize to IEnumerable<PromotionModel> (JsonConvert) using a continuation flow
            var promotions = Get("promotions");

            // TODO:
            // Await both asynchronous jobs
            // Continue when both are done
            // Match products/promotions by category id
            // Build strings with promotion name, product name and discount percentage
            return await Task.FromResult(new[] { "$[Summer sales] Antwerp University Sweater is now at 20% discount!" });
        }

        // TODO (bonus):
        // Add a CancellationToken as 2nd argument to stop the loop as soon as a cancellation is requested
        public void PollForCheapProducts(int intervalMilliseconds)
        {
            Task.Run(async () =>
            {
                while (true)
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
            return await Task.FromResult(string.Empty);
        }
    }
}
