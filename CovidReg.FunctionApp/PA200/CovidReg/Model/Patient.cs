using System;
using System.Collections.Generic;
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
            Appointments = new List<DateTime>();
        }
        
        public Patient(string partitionKey, string rowKey, string name, string email, List<DateTime> appointments)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Name = name;
            Email = email;
            Appointments = new List<DateTime>(appointments);
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }
        
        public List<DateTime> Appointments { get; set; }
    }
}