using CovidReg.FunctionApp.Pa200.Covidreg.Services;
using CovidReg.FunctionApp.Pa200.CovidReg.Services;
using CovidReg.FunctionApp.PA200.CovidReg.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CovidReg.FunctionApp.PA200.CovidReg.Startup))]

namespace CovidReg.FunctionApp.PA200.CovidReg
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();
        }
    }
}