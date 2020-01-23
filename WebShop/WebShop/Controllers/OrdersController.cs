using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data.Context;
using WebShop.Data.Entities;
using WebShop.Models;
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

        [HttpPost("sequential")]
        public async Task<ActionResult<string>> PostOrder(OrderCreateModel order)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            await _logger.LogAsync();
            await PostToDatabaseAsync(order);
            await _mailer.MailAsync();

            timer.Stop();

            return $"Response time: {timer.ElapsedMilliseconds}";
        }

        [HttpPost("concurrent")]
        public async Task<ActionResult<string>> PostOrderConcurrent(OrderCreateModel order)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            //TODO CONCURRENCY 1: execute the following actions concurrently. Compare response time with the previous results


            await Task.WhenAll(_logger.LogAsync(), PostToDatabaseAsync(order), _mailer.MailAsync());

            timer.Stop();

            return $"Response time: {timer.ElapsedMilliseconds}";
        }

        [HttpPost("parallel")]
        public async Task<ActionResult<string>> PostOrderParallel(OrderCreateModel order)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            //TODO PARALLEL 1: execute the following actions in parallel. Compare response time with the original response time
            var logTask = _logger.LogAsync();
            var dbTask = PostToDatabaseAsync(order);
            var mailTask = _mailer.MailAsync();

            var taskList = new List<Task> { logTask, dbTask, mailTask };

            Parallel.ForEach(taskList, (task) =>
            {
                task.Wait();
            });

            timer.Stop();

            return $"Response time: {timer.ElapsedMilliseconds}";
        }

        [HttpPost("concurrent-fireforget")]
        public async Task<ActionResult<string>> PostOrderConcurrentFireForget(OrderCreateModel order)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            //TODO CONCURRENCY 2: execute the following actions concurrently using a fire and forget strategy. Compare response time with the previous concurrent response time
            Task.Run(async () =>
            {
                await Task.WhenAll(_logger.LogAsync(), PostToDatabaseAsync(order), _mailer.MailAsync());
            });

            timer.Stop();

            return $"Response time: {timer.ElapsedMilliseconds}";
        }

        [HttpPost("parallel-fireforget")]
        public async Task<ActionResult<string>> PostOrderParallelFireForget(OrderCreateModel order)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            //TODO PARALLEL 2: execute the following actions in parallel using a fire and forget strategy. Compare response time with the previous parallel response time
            await _logger.LogAsync();
            await PostToDatabaseAsync(order);
            await _mailer.MailAsync();

            timer.Stop();

            return $"Response time: {timer.ElapsedMilliseconds}";
        }

        [HttpPost("job-persistence")]
        public async Task<ActionResult<string>> PostOrderConcurrentQueueing(OrderCreateModel order)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            //TODO Job persistence 1: execute the following actions concurrently using a queueing strategy. Compare response time with the previous concurrent response time
            BackgroundJob.Enqueue(() => _logger.LogAsync());
            BackgroundJob.Enqueue(() => PostToDatabaseAsync(order));
            BackgroundJob.Enqueue(() => _mailer.MailAsync());

            timer.Stop();

            return $"Response time: {timer.ElapsedMilliseconds}";
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task PostToDatabaseAsync(OrderCreateModel order)
        {
            await _dbContext.Orders.AddAsync(new Order
            {
                Name = order.Name,
                Address = order.Address
            });
        }
    }
}
