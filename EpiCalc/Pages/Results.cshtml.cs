using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EpiCalc.Models;
using EpiCalc.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace EpiCalc.Pages
{
    public class ResultsModel : PageModel
    {
        private readonly ResultService _resultService;
        public List<Result> TenResults;
        public List<string> LastTen =new();
        public string Result;
        public string Header;

        public ResultsModel(ResultService resultService)
        {
            _resultService = resultService;
        }

        public async Task<IActionResult> OnGet(Result result)
        {
            // Get top 10 add to list
            TenResults = GetTopTen();
            Log.Information($"First result top 10{TenResults[0].Calculation}");
            foreach (var r in TenResults)
            {
                LastTen.Add(r.Calculation);
            }

            // View result and write to Db
            Header = "Your calculation result:";
            Result = result.Calculation;
            Log.Information($"Result calculation in Results page {Result}");
            // Writes result to Db
            var responseMessage = await AddResult(result);

            Log.Information($"response from Db {responseMessage}");

            return Page();
        }

        public async Task<string> AddResult(Result result)
        {
            var responseMessage = await _resultService.Create(result);
            return responseMessage.Calculation;
        }

        public List<Result> GetTopTen()
        {
            var response = _resultService.Get();
            var shortList = new List<Result>();
            if (response.Count > 9)
            {
                shortList = response
                    .OrderByDescending(x => x.Timestamp)
                    .Take(10)
                    .ToList();
            }
            else
            {
                shortList = response
                    .OrderByDescending(x => x.Timestamp)
                    .ToList();
            }
           
            return shortList;
        }
    }
}