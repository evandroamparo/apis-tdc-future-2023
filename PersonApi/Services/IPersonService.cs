using PersonApi.Models;

namespace PersonApi.Services
{
    public interface IPersonService
    {
        Person GetPersonById(int personId);
        Person CreatePerson(string name);
    }
}