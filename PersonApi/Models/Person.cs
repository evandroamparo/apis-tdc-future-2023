namespace PersonApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; private set; }
        public DateOnly? BirthDate { get; private set; }

        public Person(string name, DateOnly? birthDate = null)
        {
            Name = name;
            BirthDate = birthDate.GetValueOrDefault();
        }
    }
}