using CovidReg.FunctionApp.PA200.CovidReg.Model;

namespace CovidReg.FunctionApp.PA200.CovidReg.Services
{
    public interface IPatientService
    {
        public void RegisterPatient(string name, string email);

        public Patient GetPatient(string email);

        public void UpdatePatient(Patient patient);
    }
}