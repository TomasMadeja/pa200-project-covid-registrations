using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CovidReg.FunctionApp.PA200.CovidReg.Exceptions;
using CovidReg.FunctionApp.PA200.CovidReg.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CovidReg.FunctionApp.PA200.CovidReg.Functions
{
    public class TimeslotCreation
    {
        private readonly IScheduleService _scheduleService;

        public TimeslotCreation(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        
        [FunctionName("TimeslotCreation")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            string location = data?.name;
            string fromDateRaw = data?.fromDate;
            string toDateRaw = data?.toDate;
            int? minutes = data?.minutes;
            int? capacity = data?.capacity;

            if (location == null || fromDateRaw == null || toDateRaw == null || !minutes.HasValue || !capacity.HasValue)
            {
                return new BadRequestObjectResult(
                    "Please pass json body with keys name (string) and capacity (int)"
                );
            }

            DateTime fromDate;
            DateTime toDate;
            try
            {
                fromDate = DateTime.Parse(
                    fromDateRaw
                );
                toDate = DateTime.Parse(
                    toDateRaw
                );
            }
            catch (FormatException)
            {
                return new BadRequestObjectResult("Provide date in proper format");
            }

            try
            {
                _scheduleService.GenerateEmptySlots(location, fromDate, toDate, minutes.Value, capacity.Value);
                return new OkObjectResult($"Slots created");
            }
            catch (LocationNotFoundException)
            {
                return new BadRequestObjectResult("Location not found");
            }
        }
    }
}