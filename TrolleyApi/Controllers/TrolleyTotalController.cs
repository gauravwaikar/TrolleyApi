using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrolleyApi.Exercise3.Domain;
using TrolleyApi.Exercise3.Services;

namespace TrolleyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyTotalController : ControllerBase
    {
        
        private readonly ILogger<TrolleyTotalController> _logger;
        private readonly ITrolleyTotalService _trolleyTotalService;

        public TrolleyTotalController(ITrolleyTotalService trolleyTotalService,
            ILogger<TrolleyTotalController> logger)
        {
            
            _logger = logger;
            _trolleyTotalService = trolleyTotalService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] CalculateTrolleyTotalRequest request)
        {
            
            _logger.LogInformation("Inside TrolleyTotal controller");
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(request));
            return Ok(_trolleyTotalService.Calculate(request));
        }
    }
}