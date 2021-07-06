using System;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using CovidReg.FunctionApp.PA200.CovidReg.Exceptions;
using CovidReg.FunctionApp.PA200.CovidReg.Model;
using CovidReg.FunctionApp.Pa200.Covidreg.Services;

namespace CovidReg.FunctionApp.Pa200.CovidReg.Services
{
    public class LocationService : ILocationService
    {
        private readonly TableClient _tableClient;
        
        public LocationService()
        {
            string tableName = "Locations";
            _tableClient  = new TableClient(
                Environment.GetEnvironmentVariable("table_connection_string"),
                tableName);
        }

        public Location GetLocation(string name)
        {
            try
            {
                return _tableClient.GetEntity<Location>(name, "").Value;
            }
            catch (RequestFailedException ex)
            {
                throw new NotFoundException($"Location {name} not found", ex);
            }
        }

        public void RegisterLocation(string name)
        {
            var location = new Location(name, "", name);
            try
            {
                _tableClient.AddEntity(location);
            }
            catch (RequestFailedException e)
            {
                throw new EntityExistsException($"Location {name} already exists", e);
            }
        }
    }
}