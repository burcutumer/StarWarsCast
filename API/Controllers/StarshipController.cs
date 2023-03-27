using API.Data.Dtos;
using API.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StarshipController :BaseApiController
    {
        private readonly IStarshipService _service;
        public StarshipController(IStarshipService service)
        {
            _service = service;

        }

        [HttpGet]
        public async Task<ActionResult<Response<List<StarshipDto>>>> GetStarships()
        {
            var result = await _service.GetStarshipsAsync();

            if (result.Data != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response<StarshipDto>>> CreateStarship([FromBody] CreateStarshipDto shipDto)
        {
            var result = await _service.CreateStarshipAsync(shipDto);

            if (result.Error != null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}