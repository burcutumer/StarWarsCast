using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Data.Dtos;
using API.Data.Entities;
using API.Data.Services;
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

        [HttpGet]
        public async Task<ActionResult<Response<List<PersonDto>>>> GetPeople()
        {
            var result = await _service.GetPeople();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var result = await _service.DeletePerson(id);
           if (result) return Ok();
            return BadRequest(new ProblemDetails{Title= "problem deleting StarWars Cast"});
        }

    }
}