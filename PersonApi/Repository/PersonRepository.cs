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

        public Person GetPersonById(int personId)
        {
            return _dbContext.Persons.FirstOrDefault(p => p.Id == personId);
        }
    }
}