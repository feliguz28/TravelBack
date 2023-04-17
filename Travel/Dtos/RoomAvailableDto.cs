namespace Travel.Dtos
{
    public class RoomAvailableDto
    {
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public string? Location { get; set; }
        public int? NumberPerson { get; set; }
    }
}
