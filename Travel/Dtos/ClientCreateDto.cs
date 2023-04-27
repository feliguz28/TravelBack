namespace Travel.Dtos
{
    public class ClientCreateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? Document { get; set; }
        public string? Email { get; set; }
        public int? ReserveId { get; set; }
    }
}
