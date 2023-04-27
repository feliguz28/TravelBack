namespace Travel.Models
{
    public class Reserve
    {
        public int ReserveId { get; set; }
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public int? NumberPersons { get; set; }
        public string? Location { get; set; }
        public int RoomId { get; set; }
        public Boolean Status { get; set; }
        public decimal Value { get; set; }

    }
}
