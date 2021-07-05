using System;
using System.IO;
using System.Threading.Tasks;
using CovidReg.FunctionApp.PA200.CovidReg.Exceptions;
using CovidReg.FunctionApp.Pa200.Covidreg.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CovidReg.FunctionApp.PA200.CovidReg.Functions
{
    public class LocationRegistration
    {
        private readonly ILocationService _locationService;

        LocationRegistration(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [FunctionName("LocationRegistration")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            string name = data?.name;
            int? capacity = data?.capacity;

            if (name == null || !capacity.HasValue)
            {
                return new BadRequestObjectResult(
                    "Please pass json body with keys name (string) and capacity (int)"
                    );
            }

            try
            {
                _locationService.RegisterLocation(name, capacity.Value);
                return new OkObjectResult($"Location {name} created");
            }
            catch (EntityExistsException ex)
            {
                return new BadRequestObjectResult("Location already exists");
            }
        }
    }
}