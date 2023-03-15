using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Data.Dtos;
using API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Services
{


    public class StarshipService : IStarshipService
    {
        private readonly StoreContext _context;

        public StarshipService(StoreContext context)
        {
            _context = context;
        }

        private static StarshipDto MapToShipDto(Starship starship)
        {
            return new StarshipDto
            {
                Id = starship.Id,
                Name = starship.Name,
                Model = starship.Model,
                Length = starship.Length,
                Pilot = new PersonDto
                {
                    Name = starship.Person.Name,
                    Id = starship.Person.Id,
                    EyeColor = starship.Person.EyeColor,
                    Height = starship.Person.Height,
                    Mass = starship.Person.Mass,
                    CreatedAt = starship.Person.CreatedAt,
                    SkinColor = starship.Person.SkinColor
                }
            };
        }

        public async Task<Response<List<StarshipDto>>> GetStarshipsAsync()
        {
            var ships = await _context.Starships
            .Include(p => p.Person)
            .Select(s => MapToShipDto(s))
            .ToListAsync();

            if (ships != null)
            {
                return new Response<List<StarshipDto>>
                {
                    Data = ships
                };
            }
            return new Response<List<StarshipDto>>
            {
                Error = "cant find Starships"
            };
        }

        public async Task<Response<StarshipDto>> CreateStarshipAsync(CreateStarshipDto shipDto)
        {
            var person = await _context.People.FindAsync(shipDto.PilotId);

            if (person == null)
            {
                return new Response<StarshipDto>
                {
                    Error = "Person is not found"
                };
            }
            var newStarship = new Starship
            {
                Person = person!,
                Name = shipDto.Name,
                Model = shipDto.Model,
                Length = shipDto.Length
            };
            await _context.Starships.AddAsync(newStarship);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                return new Response<StarshipDto>
                {
                    Error = "could not create starship"
                };
            }
            return new Response<StarshipDto>
            {
                Data = MapToShipDto(newStarship)
            };
        }
    }
}