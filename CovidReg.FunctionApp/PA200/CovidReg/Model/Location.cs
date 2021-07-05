using System;
using Azure;
using Azure.Data.Tables;

namespace CovidReg.FunctionApp.PA200.CovidReg.Model
{
    public class Location : ITableEntity
    {
        public Location()
        {
            
        }

        public Location(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        public Location(string partitionKey, string rowKey, string name, int capacity)
        {
            Name = name;
            PartitionKey = partitionKey;
            Capacity = capacity;
            RowKey = rowKey;
        }

        private string Name { get; set; }
        public string PartitionKey { get; set; }
        private int Capacity { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}