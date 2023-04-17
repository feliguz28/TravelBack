namespace Travel.Dtos
{
    public class HotelUpdateStatusDto
    {
        public int? HotelId { get; set; }
        public Boolean? Status { get; set; }
        public int? UpdateUser { get; set; }
    }
}
