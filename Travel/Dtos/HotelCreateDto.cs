namespace Travel.Dtos
{
    public class HotelCreateDto
    {
        public string? HotelName { get; set; }
        public string? HotelLocalion { get; set; }
        public Decimal? Value { get; set; }
        public string? UrlImage { get; set; }
        public Boolean? Status { get; set; }
        public int? CreatedUser { get; set; }
    }
}
