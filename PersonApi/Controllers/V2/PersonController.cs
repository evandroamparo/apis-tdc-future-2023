using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using PersonApi.Services;

namespace PersonApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public ActionResult<Person> Get(int id)
        {
            var person = _personService.GetPersonById(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        public ActionResult<Person> Post([FromBody] PersonCreateModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid input");
            }

            var newPerson = _personService.CreatePerson(model.Name, DateOnly.FromDateTime(model.BirthDate), model.TaxId);

            return CreatedAtAction(nameof(Get), new { id = newPerson.Id }, newPerson);
        }
    }
}