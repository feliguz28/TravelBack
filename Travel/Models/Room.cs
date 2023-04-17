namespace Travel.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomDescription { get; set; }
        public Boolean Status { get; set; }
        public int Value { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int CreatedUser { get; set; }
        public int LastupdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? DeleteAt { get; set; }
        public string? Location { get; set; }
        public string? UrlImage { get; set; }
    }
}
