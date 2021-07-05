using System;
using Azure;
using Azure.Data.Tables;

namespace CovidReg.FunctionApp.PA200.CovidReg.Model
{
    public class ReservationSlot : ITableEntity
    {
        public ReservationSlot()
        {
        }

        public ReservationSlot(string partitionKey, string rowKey, DateTime reservationDate, int currentCapacity)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            ReservationDate = reservationDate;
            CurrentCapacity = currentCapacity;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        
        public DateTime ReservationDate { get; set; }
        public int CurrentCapacity { get; set; }
    }
}