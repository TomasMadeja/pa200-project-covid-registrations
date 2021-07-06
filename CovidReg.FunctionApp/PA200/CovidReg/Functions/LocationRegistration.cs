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

            if (name == null)
            {
                return new BadRequestObjectResult(
                    "Please pass json body with keys name (string)"
                    );
            }

            try
            {
                _locationService.RegisterLocation(name);
                return new OkObjectResult($"Location {name} created");
            }
            catch (EntityExistsException)
            {
                return new BadRequestObjectResult("Location already exists");
            }
        }
    }
}