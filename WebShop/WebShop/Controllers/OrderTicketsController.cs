﻿using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTicketsController : ControllerBase
    {
        public OrderTicketsController()
        {
        }

        //TODO Memory leak 1: Try to call the first endpoint a few times and watch what happens. How should we solve this?
        //TIP: idisposableanalyzers

        [HttpGet("generateorders")]
        public ActionResult<string> Generate()
        {
            Stream _stream;
            for (int i = 0; i < 100; i++)
            {
                _stream = new FileStream($"ordertickets/order{i}.txt", FileMode.OpenOrCreate);
            }

            return $"This is not OK";
        }
    }
}
