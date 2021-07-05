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
    public class RegisterVaccination
    {
        private readonly IScheduleService _scheduleService;

        public RegisterVaccination(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        
        [FunctionName("RegisterVaccination")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string location = data?.location;
            string email = data?.email;
            string dateString = data?.date;

            if (location == null || email == null || dateString == null)
            {
                return new BadRequestObjectResult(
                    "Please pass an email (string) and date (ISO string) and location (string) in request body"
                    );
            }

            DateTime date = DateTime.ParseExact(
                dateString, 
                "o", 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None
                );

            try
            {
                _scheduleService.RegisterVaccination(location, email, date);
                return new OkObjectResult("Reservation successful");
            }
            catch (PatientNotFoundException)
            {
                return new BadRequestObjectResult("Patient not found");
            }
            catch (FullSlotException)
            {
                return new BadRequestObjectResult("Slot is already full");
            }
            catch (AlreadyRegisteredException)
            {
                return new BadRequestObjectResult("Already registered for vaccination");
            }
        }
    }
}