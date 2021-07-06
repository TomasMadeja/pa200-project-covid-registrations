namespace CovidReg.FunctionApp.PA200.CovidReg.Model
{
    public class ReservationSlotDto
    {
        public ReservationSlotDto(string date, int currentCapacity)
        {
            this.Date = date;
            this.CurrentCapacity = currentCapacity;
        }

        public string Date { get; set; }
        public int CurrentCapacity { get; set; }
        
    }
}