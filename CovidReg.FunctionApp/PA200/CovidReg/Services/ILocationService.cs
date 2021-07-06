using CovidReg.FunctionApp.PA200.CovidReg.Model;

namespace CovidReg.FunctionApp.Pa200.Covidreg.Services
{
    public interface ILocationService
    {
        public Location GetLocation(string name);

        public void RegisterLocation(string name);
    }
}