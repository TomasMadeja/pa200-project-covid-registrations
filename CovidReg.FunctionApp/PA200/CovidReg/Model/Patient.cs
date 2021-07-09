using System;
using System.Collections.Generic;
using System.Text.Json;
using Azure;
using Azure.Data.Tables;

namespace CovidReg.FunctionApp.PA200.CovidReg.Model
{
    public class Patient : ITableEntity
    {
        public Patient()
        {
        }

        public Patient(string partitionKey, string rowKey, string name, string email)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Name = name;
            Email = email;
            Appointments = JsonSerializer.Serialize(new List<string>());
        }
        
        public Patient(string partitionKey, string rowKey, string name, string email, List<string> appointments)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Name = name;
            Email = email;
            Appointments = JsonSerializer.Serialize(appointments);
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }
        
        public string Appointments { get; set; }

        public List<string> GetAppointments() {
            return JsonSerializer.Deserialize<List<string>>(Appointments);
        }

        public void SetAppointments(List<string> appointments) {
            Appointments = JsonSerializer.Serialize(appointments);
        }
    }
}