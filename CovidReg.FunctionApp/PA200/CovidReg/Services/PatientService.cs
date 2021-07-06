using System;
using Azure;
using Azure.Data.Tables;
using CovidReg.FunctionApp.PA200.CovidReg.Exceptions;
using CovidReg.FunctionApp.PA200.CovidReg.Model;

namespace CovidReg.FunctionApp.PA200.CovidReg.Services
{
    public class PatientService : IPatientService
    {
        private readonly TableClient _tableClient;
        
        public PatientService()
        {
            string tableName = "Patients";
            _tableClient  = new TableClient(
                Environment.GetEnvironmentVariable("table_connection_string"),
                tableName);
        }

        public Patient GetPatient(string email) 
        {
            try
            {
                return _tableClient.GetEntity<Patient>(email, "").Value;
            }
            catch (RequestFailedException ex)
            {
                throw new NotFoundException($"Patient {email} not found", ex);
            }
        }

        public void RegisterPatient(string name, string email)
        {
            var patient = new Patient(name, "", name, email);
            try
            {
                _tableClient.AddEntity(patient);
            }
            catch (RequestFailedException e)
            {
                throw new EntityExistsException($"Patient for email {email} already exists", e);
            }
        }
        
        public void UpdatePatient(Patient patient)
        {
            _tableClient.UpsertEntity(patient);
        }
    }
}