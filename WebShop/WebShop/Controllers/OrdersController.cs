using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data.Context;
using WebShop.Data.Entities;
using WebShop.Shared.Logger;
using WebShop.Shared.Mailer;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IDummyLogger _logger;
        private readonly IDummyMailer _mailer;
        private readonly WebShopContext _dbContext;

        public OrdersController(
            IDummyLogger logger,
            IDummyMailer mailer,
            WebShopContext context
            )
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _mailer = mailer ?? throw new System.ArgumentNullException(nameof(mailer));
            _dbContext = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        [HttpPost()]
        public async Task<ActionResult<string>> PostOrder(Order order)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            //TODO PARALLEL 1: execute the following actions in parallel. Compare response time with the original response time
            //TODO CONCURRENCY 1: execute the following actions concurrently. Compare response time with the previous results
            //TODO PARALLEL 2: execute the following actions in parallel using a fire and forget strategy. Compare response time with the previous parallel response time
            //TODO CONCURRENCY 2: execute the following actions concurrently using a fire and forget strategy. Compare response time with the previous concurrent response time
            //TODO PARALLEL 3: execute the following actions in parallel using a queueing strategy. Compare response time with the previous parallel response time
            //TODO CONCURRENCY 3: execute the following actions concurrently using a queueing strategy. Compare response time with the previous concurrent response time
            await _logger.LogAsync();
            await PostToDatabaseAsync(order);
            await _mailer.MailAsync();

            timer.Stop();

            return $"Response time: {timer.ElapsedMilliseconds}";
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task PostToDatabaseAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }
    }
}
