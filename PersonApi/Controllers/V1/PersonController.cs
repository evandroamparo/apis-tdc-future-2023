using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using PersonApi.Services;

namespace PersonApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [MapToApiVersion("1.0")]
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

        [MapToApiVersion("1.0")]
        [HttpPost]
        public ActionResult<Person> Post([FromBody] PersonCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid input");
            }

            var newPerson = _personService.CreatePerson(request.Name, DateOnly.FromDateTime(request.BirthDate), request.TaxId);

            return CreatedAtAction(nameof(Get), new { id = newPerson.Id }, newPerson);
        }
    }
}