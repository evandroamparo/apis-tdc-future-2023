using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using PersonApi.Services;

namespace PersonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            var person = _personService.GetPersonById(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost("create")]
        public ActionResult<Person> Post(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Invalid input");
            }

            var newPerson = _personService.CreatePerson(name);

            return CreatedAtAction(nameof(Get), new { id = newPerson.Id }, newPerson);
        }

        [HttpPost("createWithBirthDate")]
        public ActionResult<Person> PostWithBirthDate(string name, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Invalid input");
            }

            var newPerson = _personService.CreatePerson(name, DateOnly.FromDateTime(birthDate));

            return CreatedAtAction(nameof(Get), new { id = newPerson.Id }, newPerson);
        }

    }
}