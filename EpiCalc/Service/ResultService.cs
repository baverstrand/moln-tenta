using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EpiCalc.Configuration;
using EpiCalc.Models;
using EpiCalc.Service;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;

namespace EpiCalc.Service
{
    public class ResultService
    {
        private readonly IMongoCollection<Result> _result;

        public ResultService(IEpiCalcDbSettings settings)
        {
            var client = new MongoClient(settings.CosmosDbConnectionString);
            var database = client.GetDatabase(settings.DbName);

            _result = database.GetCollection<Result>(settings.DbName);
        }

        public List<Result> Get()
        {
           var result =  _result.Find(result => true).ToList();
           return result;
        }

        public async Task<Result> Create(Result result)
        {
            Log.Information($"Database going in: {result.Calculation}");
            await _result.InsertOneAsync(result);
            return result;
        }
    }
}
