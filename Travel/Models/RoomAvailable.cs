namespace Travel.Models
{
    public class RoomAvailable
    {
        public int? RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomDescription { get; set; }
        public Decimal? Value { get; set; }
        public string? Location { get; set; }
        public string? RoomUrlImage { get; set; }
        public string? RoomTypeName { get; set; }
        public int? MaxPeople { get; set; }
        public int? HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? HotelUrlImage { get; set; }
    }
}
