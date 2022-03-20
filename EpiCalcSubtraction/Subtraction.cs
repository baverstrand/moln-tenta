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

namespace EpiCalcSubtraction
{
    public static class Subtraction
    {
        [FunctionName("Subtraction")]
        public static ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string first = req.Query["first"];
            string second = req.Query["second"];

            int varone;
            int vartwo;
            if (!int.TryParse(first, out varone) || !int.TryParse(second, out vartwo))
            {
                return new BadRequestObjectResult("No valid input. Please try again.");
            }

            var result = $"{varone} - {vartwo} = {(varone - vartwo).ToString(CultureInfo.InvariantCulture)}";
            return new OkObjectResult(result);
        }
    }
}
