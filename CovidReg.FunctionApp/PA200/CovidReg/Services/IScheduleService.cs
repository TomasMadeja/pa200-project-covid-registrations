using System;
using System.Collections.Generic;
using CovidReg.FunctionApp.PA200.CovidReg.Model;

namespace CovidReg.FunctionApp.PA200.CovidReg.Services
{
    public interface IScheduleService
    {
        public void RegisterVaccination(string location, string email, DateTime firstDate);

        public IEnumerator<ReservationSlot> GetEmptySlots(string location, DateTime fromDate, DateTime toDate);
    }
}