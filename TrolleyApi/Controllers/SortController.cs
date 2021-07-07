using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrolleyApi.Exercise2.Services;

namespace TrolleyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly ISortService _sortService;

        public SortController(ISortService sortService)
        {
            _sortService = sortService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get(
            [FromQuery]
            [Required]
            [SortOptionValidator]
        string sortOption)
            => Ok(await _sortService.Sort(sortOption.ToUpperInvariant()));
    }
}
