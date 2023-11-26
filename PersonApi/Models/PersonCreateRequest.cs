namespace PersonApi.Models
{
    public class PersonCreateRequest
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? TaxId { get; set; }
    }
}