namespace Travel.Models
{
    public class Hotel
    {
        public int    HotelId               { get; set; }
        public string? HotelName            { get; set; }
        public string? HotelLocalion         { get; set; }
        public Decimal? Value                { get; set; }
        public string? UrlImage             { get; set; }
        public Boolean Status               { get; set; }
        public int CreatedUser              { get; set; }
        public int LastupdatedUser          { get; set; }
        public DateTime? CreatedDate         { get; set; }
        public DateTime? LastUpdatedDate     { get; set; }
        public DateTime? DeleteAt           { get; set; }
    }
}
