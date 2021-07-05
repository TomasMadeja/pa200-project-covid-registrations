using System;
using System.Collections.Generic;
using System.Linq;
using Azure;
using Azure.Data.Tables;
using CovidReg.FunctionApp.PA200.CovidReg.Exceptions;
using CovidReg.FunctionApp.PA200.CovidReg.Model;

namespace CovidReg.FunctionApp.PA200.CovidReg.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly int NEXT_VACCINATION_DAY_COUNT = 42;
        
        private readonly TableClient _scheduleTable;
        private readonly IPatientService _patientService;
        
        public ScheduleService(IPatientService patientService)
        {
            _patientService = patientService;
            
            string tableName = "Scheadule";
            _scheduleTable  = new TableClient(
                new Uri(""),
                tableName,
                new TableSharedKeyCredential("", ""));
        }

        public void RegisterVaccination(string location, string email, DateTime firstDate)
        {
            ReservationSlot slot = GetReservationSlot(location, firstDate);
            if (slot.CurrentCapacity == 0)
            {
                throw new FullSlotException($"Slot for {firstDate} is full");
            }

            Patient patient;
            try
            {
                patient = _patientService.GetPatient(email);
            }
            catch (NotFoundException ex)
            {
                throw new PatientNotFoundException($"Patient registered to ${email} not found", ex);
            }

            if (patient.Appointments.Count != 0)
            {
                throw new AlreadyRegisteredException($"Patient already registered");
            }
            
            var appointments = new List<DateTime>();
            appointments.Add(firstDate);
            appointments.Add(firstDate.AddDays(NEXT_VACCINATION_DAY_COUNT));
            patient.Appointments = appointments;
            
            slot.CurrentCapacity--;
            _scheduleTable.UpsertEntity(slot);

            try
            {
                _patientService.UpdatePatient(patient);
            }
            catch (Exception)
            {
                slot.CurrentCapacity++;
                _scheduleTable.UpsertEntity(slot);
                throw;
            }
        }

        public IEnumerator<ReservationSlot> GetEmptySlots(string location, DateTime fromDate, DateTime toDate)
        {
            return _scheduleTable.Query<ReservationSlot>(
                $"PartitionKey eq {location} and RowKey ge {fromDate:o} and RowKey le {toDate:o}"
                ).GetEnumerator();
        }

        private ReservationSlot GetReservationSlot(string location, DateTime date)
        {
            try
            {
                return _scheduleTable.GetEntity<ReservationSlot>(location, date.ToString("O")).Value;
            }
            catch (RequestFailedException ex)
            {
                throw new NotFoundException($"Reservation slot for {date} not found", ex);
            }
        }
    }
}