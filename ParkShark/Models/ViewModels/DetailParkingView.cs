namespace ParkShark.Models.ViewModels
{
    public class DetailParkingView
    {
        public int ParkingId { get; set; }
        public string TransportationTypeName { get; set; }
        public string LicensePlate { get; set; }
        public DateTime TimeEntry { get; set; }
        public int HourlyRate { get; set; }
        public decimal ParkingFee { get; set; }
        public string Status { get; set; }
    }
}
