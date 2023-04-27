namespace Travel.Dtos
{
    public class ReserveCreateDto
    {
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public int NumberPerson { get; set; }
        public string? Location { get; set; }
        public int? RoomId { get; set; }
        public Boolean? Status { get; set; }
        public decimal? Value { get; set; }
        public int? CreatedUser { get; set; }
    }
}
