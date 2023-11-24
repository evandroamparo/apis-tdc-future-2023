namespace PersonApi.Models
{
    public class PersonCreateModel
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? TaxId { get; set; }
    }
}