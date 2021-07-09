using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CovidReg.FunctionApp.PA200.CovidReg.Model;
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
    public class ReservationSlotGetter
    {
        private readonly IScheduleService _scheduleService;

        public ReservationSlotGetter(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [FunctionName("ReservationSlotGetter")]
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

            if (location == null || fromDateRaw == null || toDateRaw == null)
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

            IEnumerable<ReservationSlot> slots;
            try {
                slots = _scheduleService.GetEmptySlots(location, fromDate, toDate);
            } catch (Exception ex) {
                log.LogError(ex.ToString());
                throw ex;
            }
            List<ReservationSlotDto> slotList = new List<ReservationSlotDto>();
            foreach (ReservationSlot slot in slots)
            {
                slotList.Add(new ReservationSlotDto(slot.ReservationDate.ToString("o"), slot.CurrentCapacity));
            }
            return new JsonResult(slotList);
        }
    }
}