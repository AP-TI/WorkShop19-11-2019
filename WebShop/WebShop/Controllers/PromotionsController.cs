using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Data.Context;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly WebShopContext _context;

        public PromotionsController(WebShopContext context)
        {
            _context = context;
        }

        // GET: api/Promotions
        [HttpGet]
        public IEnumerable<PromotionModel> Get()
        {
            return _context.Promotions.Include(x => x.Category).Select(x => new PromotionModel(x));
        }
    }
}
