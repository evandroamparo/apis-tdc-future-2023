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

        public Person CreatePerson(string name, DateOnly birthDate)
        {
            var newPerson = new Person(name, birthDate);

            _personRepository.AddPerson(newPerson);

            return newPerson;
        }

        public Person GetPersonById(int personId)
        {
            return _personRepository.GetPersonById(personId);
        }
    }
}