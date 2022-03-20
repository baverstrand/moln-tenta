using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EpiCalcAddition
{
    public static class Addition
    {
        [FunctionName("Addition")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string first = req.Query["first"];
            string second = req.Query["second"];
            
            if (!int.TryParse(first, out var varone) || !int.TryParse(second, out var vartwo))
            {
                return new BadRequestObjectResult("No valid input. Please try again.");
            }
            var result = $"{varone} + {vartwo} = {(varone + vartwo).ToString(CultureInfo.InvariantCulture)}";
            return new OkObjectResult(result);
        }
    }
}
