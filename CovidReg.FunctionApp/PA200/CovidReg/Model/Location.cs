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

        public Location(string name)
        {
            Name = name;
        }

        public Location(string partitionKey, string rowKey, string name)
        {
            Name = name;
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string Name { get; set; }
    }
}