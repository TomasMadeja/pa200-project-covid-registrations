using System;
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
    public class PatientRegistration
    {
        private readonly IPatientService _patientService;

        public PatientRegistration(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [FunctionName("PatientRegistration")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            string name = data?.name;
            string email = data?.email;

            if (name == null || email == null)
            {
                return new BadRequestObjectResult(
                    "Please pass json body with keys name (string) and email (string)"
                );
            }

            try
            {
                _patientService.RegisterPatient(name, email);
                return new OkObjectResult($"User {email} created");
            }
            catch (EntityExistsException ex)
            {
                log.LogInformation(ex.ToString());
                return new BadRequestObjectResult("User already exists");
            }
            
        }
    }
}