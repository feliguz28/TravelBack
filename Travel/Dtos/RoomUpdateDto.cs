namespace Travel.Dtos
{
    public class RoomUpdateDto
    {
        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomDescription { get; set; }
        public Boolean Status { get; set; }
        public int Value { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int UpdateUser { get; set; }
        public string? Location { get; set; }
        public string? UrlImage { get; set; }
    }
}
