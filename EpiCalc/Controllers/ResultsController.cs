using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpiCalc.Service;

namespace EpiCalc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ResultService _resultService;

        public ResultsController(ResultService resultService)
        {
            _resultService = resultService;
        }
    }
}
