namespace Travel.Models
{
    public class PagerRequest
    {
        public int pageNumber { get; set; }
        public int registerPage { get; set; }
        public string? filter { get; set; }
    }
}
