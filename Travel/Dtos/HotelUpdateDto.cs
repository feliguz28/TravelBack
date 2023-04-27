namespace Travel.Dtos
{
    public class HotelUpdateDto
    {
        public int? HotelId  { get; set; }
        public string? HotelName { get; set; }
        public string? HotelLocation { get; set; }
        public Decimal? Value { get; set; }
        public string? UrlImage { get; set; }
        public Boolean? Status { get; set; }
        public int? UpdateUser { get; set; }
    }
}
