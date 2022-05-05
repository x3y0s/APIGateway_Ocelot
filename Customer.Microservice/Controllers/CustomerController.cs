using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Microservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IBroker _broker;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            IBroker broker,
            ILogger<CustomerController> logger)
        {
            _broker = broker;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _broker.Enqueue("Customer was called");

            return Ok("Customer Microservice executed !");
        }
    }
}