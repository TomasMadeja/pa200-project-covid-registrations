using System;
using System.Collections.Generic;
using Azure;
using Azure.Data.Tables;

namespace CovidReg.FunctionApp.PA200.CovidReg.Model
{
    public class Reservation : ITableEntity
    {
        public Reservation()
        {
        }

        public Reservation(string partitionKey, string rowKey, string email, List<DateTime> appointments)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Email = email;
            Appointments = new List<DateTime>(appointments);
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        
        private string Email { get; set; }
        private List<DateTime> Appointments { get; set; }
    }
}