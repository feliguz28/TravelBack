namespace Travel.Dtos
{
    public class RoomCreateDto
    {
        public string? RoomName { get; set; }
        public string? RoomDescription { get; set; }
        public Boolean Status { get; set; }
        public int Value { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int CreatedUser { get; set; }
        public string? Location { get; set; }
        public string? UrlImage { get; set; }
    }
}
