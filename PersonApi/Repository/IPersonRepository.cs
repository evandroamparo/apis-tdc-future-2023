using PersonApi.Models;

namespace PersonApi.Repository
{
    public interface IPersonRepository
    {
        Person GetPersonById(int personId);
        void AddPerson(Person person);
    }
}