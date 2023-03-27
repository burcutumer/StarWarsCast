using API.Data.Dtos;
using API.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PeopleController : BaseApiController
    {
        private readonly IPeopleService _service;

        public PeopleController(IPeopleService service)
        {
            _service = service;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<PersonDto>>> GetPerson(int id)
        {
            var result = await _service.GetPerson(id);

            if (result.Data != null) return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<PersonDto>>>> GetPeople()
        {
            var result = await _service.GetPeople();
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var result = await _service.DeletePerson(id);

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "problem deleting StarWars Cast" });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Response<PersonDto>>> CreatePerson([FromBody] CreatePersonDto person)
        {
            var result = await _service.CreatePerson(person);

            if (result.Data != null) return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<PersonDto>>> UpdatePerson([FromBody] UpdatePersonDto person, int id)
        {
            var result = await _service.UpdatePerson(person, id);

            if (result.Data != null) return Ok(result);

            return BadRequest(result);
        }
    }
}