using API.Data.Dtos;

namespace API.Data.Services
{
    public interface IStarshipService
    {
        Task<Response<StarshipDto>> CreateStarshipAsync(CreateStarshipDto shipDto);
        Task<Response<List<StarshipDto>>> GetStarshipsAsync();
    }
}