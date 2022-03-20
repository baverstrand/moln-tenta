using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EpiCalc.Configuration;
using EpiCalc.Models;
using EpiCalc.Service;
using Microsoft.Extensions.Options;
using Serilog;

namespace EpiCalc.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty, Required]
        public int First { get; set; }
        [BindProperty, Required]
        public int Second { get; set; }
        [BindProperty, Required]
        public string ArithmeticOperation { get; set; }

        private readonly ApiSettings _apiSettings;

        public IndexModel(IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = CreateRequest(First, Second);
            string responseMessage;

            if (ArithmeticOperation == "Addition")
            {
                Log.Information($"arithmetic operation addition");
                responseMessage = await Addition(request);
            }
            else if (ArithmeticOperation == "Subtraction")
            {
                Log.Information($"arithmetic operation subtraction");
                responseMessage = await Subtraction(request);
            }
            else
            {
                var message = "Arithmetic operator missing. Please try again.";
                Log.Warning(message);
                return new BadRequestObjectResult(message);
            }
            var result = CreateResult(responseMessage);
            return RedirectToPage("Results", result);
        }

        public async Task<string> Addition(string request)
        {
            var baseUrl = _apiSettings.EpiCalcAddition;
            Log.Information($"baseUrl{baseUrl}");
            var requestUrl = baseUrl + request;
            Log.Information($"requestUrl{requestUrl}");
            var client = new HttpClient();
            var response = await client.GetAsync(requestUrl);
            var responseContent = await response.Content.ReadAsStringAsync();
            Log.Information($"Response content {responseContent}");
            return responseContent;
        }

        public async Task<string> Subtraction(string request)
        {
            var baseUrl = _apiSettings.EpiCalcSubtraction;
            Log.Information($"baseUrl{baseUrl}");
            var requestUrl = baseUrl + request;
            Log.Information($"requestUrl{requestUrl}");
            var client = new HttpClient();
            var response = await client.GetAsync(requestUrl);
            var responseContent = await response.Content.ReadAsStringAsync();
            Log.Information($"Response content{responseContent}");
            return responseContent;
        }

        public Result CreateResult(string responseMessage)
        {
            var result = new Result()
            {
                Calculation = responseMessage
            };
            Log.Information($"result content{result.Calculation}");
            return result;
        }

        public string CreateRequest(int first, int second)
        {
            var request = $"?first={first}&second={second}";
            Log.Information($"Request tail{request}");
            return request;
        }
    }
}
