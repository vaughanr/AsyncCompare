using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncCompare.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCompare.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly CalculationService calculationService;

        public CalculationController(CalculationService calculationService)
        {
            this.calculationService = calculationService;
        }

        [HttpGet]
        [Route("sync/{num}")]
        public IActionResult Sync(int num)
        {
            var dbl = calculationService.Double(num);

            var half = calculationService.Halve(num);

            return Ok(dbl + half);
        }

        [HttpGet]
        [Route("syncoverasync/{num}")]
        public IActionResult SyncOverAsync(int num)
        {
            var dbl = calculationService.DoubleAsync(num).Result;

            var half = calculationService.HalveAsync(num).Result;

            return Ok(dbl + half);
        }

        [HttpGet]
        [Route("parallel/{num}")]
        public IActionResult ParallelCalc(int num)
        {
            int dbl = 0;
            int half = 0;
            Parallel.Invoke(
                 () => dbl = calculationService.Double(num),
                 () => half = calculationService.Halve(num)
                );

            return Ok(dbl + half);
        }

        [HttpGet]
        [Route("async/{num}")]
        public async Task<IActionResult> Async(int num)
        {
            var dbl = await calculationService.DoubleAsync(num);
            var half = await calculationService.HalveAsync(num);

            return Ok(dbl + half);
        }

        [HttpGet]
        [Route("asyncwhenall/{num}")]
        public async Task<IActionResult> AsyncWhenAll(int num)
        {
            var dblTask = calculationService.DoubleAsync(num);
            var halfTask = calculationService.HalveAsync(num);

            var results = await Task.WhenAll(dblTask, halfTask);
            return Ok(results.Sum());
        }
    }
}