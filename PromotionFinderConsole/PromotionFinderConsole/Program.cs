using System;

namespace PromotionFinderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseUrl = "https://localhost:5001/api/";
            var promotionFinder = new PromotionFinder(baseUrl);

            // TODO (bonus):
            // Provide a cancellation token
            promotionFinder.PollForCheapProducts(2000);

            Console.WriteLine("Press any key to stop polling");
            Console.ReadKey();

            // TODO (bonus):
            // Cancel the polling loop

            Console.WriteLine("Press any key to close the program");
            Console.ReadKey();
        }
    }
}
