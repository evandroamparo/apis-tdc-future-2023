using PersonApi.Models;
using PersonApi.Repository;

namespace PersonApi.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person GetPersonById(int personId)
        {
            return _personRepository.GetPersonById(personId);
        }
    }
}