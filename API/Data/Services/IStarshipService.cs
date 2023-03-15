using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Dtos;

namespace API.Data.Services
{
    public interface IStarshipService
    {
        Task<Response<StarshipDto>> CreateStarshipAsync(CreateStarshipDto shipDto);
        Task<Response<List<StarshipDto>>> GetStarshipsAsync();
    }
}