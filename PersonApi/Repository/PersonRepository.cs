using PersonApi.Data;
using PersonApi.Models;

namespace PersonApi.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _dbContext;

        public PersonRepository(PersonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddPerson(Person person)
        {
            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();
        }

        public Person GetPersonById(int personId)
        {
            return _dbContext.Persons.FirstOrDefault(p => p.Id == personId);
        }
    }
}