using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Data.Context;
using WebShop.Data.Entities;
using WebShop.FilterModels;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly WebShopContext _dbContext;

        public ProductsController(WebShopContext context)
        {
            _dbContext = context;
        }

        [HttpGet()]
        public ActionResult<List<ProductModel>> Search([FromQuery] ProductFilter filter)
        {
            if (filter == null || filter.IsEmpty)
                return Ok(_dbContext.Products.ToList());

            IQueryable<Product> products = Enumerable.Empty<Product>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.NameContains))
            {
                //TODO LINQ 3: prevent this SQL query from being executed multiple times (multiple solutions)
                products = _dbContext.Products.Where(x => x.Name.Contains(filter.NameContains));
            }

            //TODO LINQ 2: prevent this SQL query from loading all product data instead of the result count
            for (int i = 0; i < products.ToList().Count(); i++)
            {
                //TODO LINQ 1: prevent this SQL query from loading all product data instead of item i (use LINQ)
                Debug.WriteLine($"User requested info about item with id {products.ToArray()[i].Id}");
            }

            return Ok(products.ToList().Select(ToProductModel));
        }




        //TODO Query 1: extend the search method by adding a filter on 'AmountInStock' (exact match). 
        //Make sure the query is only executed once, and execution happens after filtering (deferred execution) !

        //TODO Query 2: improve performance by using async await

        //TODO Query 3: since we only need read access to get our results, disable changetracking to improve performance even further

        //TODO Query 4: Descriptions tend to be huge in size, and we don't need them in this situation. 
        //Try to exclude description retrieval from your query

        //TODO Query 5: use the page and pagesize argument and apply the paging to the filter
        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchBetter([FromQuery] int page, [FromQuery] int pagesize, [FromQuery] ProductFilter filter)
        {
            if (filter == null)
                return Ok(await _dbContext.Products.ToListAsync());

            IQueryable<Product> productQueryable = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.NameContains))
            {
                productQueryable = productQueryable.Where(x => x.Name.Contains(filter.NameContains));
            }
            var results = productQueryable
            .ToList();

            return Ok(results.Select(ToProductModel));
        }

        //TODO Query 6: execute the following action. Configure warnings for QueryClientEvaluationWarning and execute again.
        //TODO Query 7: fix the following code to search for lowercased names without using client evaluation. Notice the difference in performance?
        [HttpGet("{name}", Name = "Name")]
        public ProductModel Get(string name)
        {
            return ToProductModel(_dbContext.Products.SingleOrDefault(p => FormatName(p.Name).Equals(name)));
        }

        private static string FormatName(string name)
        {
            name = name.ToLower();
            return name;
        }

        private static ProductModel ToProductModel(Product product)
        {
            if (product == null)
                return null;
            return new ProductModel(product);
        }
    }
}
