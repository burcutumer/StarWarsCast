using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Dtos;
using API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly StoreContext _context;

        public PeopleService(StoreContext context)
        {
            _context = context;
        }

        public async Task<Response<PersonDto>> CreatePerson(CreatePersonDto person)
        {
            if (person != null)
            {
                var newp = new Person
                {
                    Name = person.Name,
                    Height = person.Height,
                    Mass = person.Mass,
                    SkinColor = person.SkinColor,
                    EyeColor = person.EyeColor,
                    CreatedAt = DateTime.UtcNow,
                };

                await _context.People.AddAsync(newp);

                var result = await _context.SaveChangesAsync() > 0;

                if (result)
                {
                    return new Response<PersonDto>
                    {
                        Data = MapToPersonDto(newp)
                    };
                }
            }

            return new Response<PersonDto>
            {
                Error = "could not create StarWars Cast Member"
            };
        }

        public async Task<bool> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person != null)
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Response<List<PersonDto>>> GetPeople()
        {
            var people = await _context.People
                .Include(o => o.Starships)
                .Select(p => MapToPersonDto(p))
                .ToListAsync();

            if (people != null)
            {
                return new Response<List<PersonDto>>
                {
                    Data = people
                };
            }
            return new Response<List<PersonDto>>
            {
                Error = "Star Wars Cast could not found"
            };
        }

        public async Task<Response<PersonDto>> GetPerson(int id)
        {
            var person = await _context.People
            .Include(p => p.Starships)
            .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return new Response<PersonDto>
                {
                    Error = "can not find Star Wars Cast Member"
                };
            }
            return new Response<PersonDto>
            {
                Data = MapToPersonDto(person)
            };
        }

        public async Task<Response<PersonDto>> UpdatePerson(UpdatePersonDto dto, int id)
        {
            var person = await _context.People.FindAsync(id);

            if (dto != null && person != null)
            {
                person.Name = dto.Name ?? person.Name;
                person.SkinColor = dto.SkinColor ?? person.SkinColor;
                person.EyeColor = dto.EyeColor ?? person.EyeColor;
                person.Height = dto.Height ?? person.Height;
                person.Mass = dto.Mass ?? person.Mass;
            }
            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return new Response<PersonDto>
                {
                    Error = "Could not update Star Wars Cast Member"
                };
            }
            return new Response<PersonDto>
            {
                Data = MapToPersonDto(person!)
            };
        }
        private static PersonDto MapToPersonDto(Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Height = person.Height,
                Mass = person.Mass,
                SkinColor = person.SkinColor,
                EyeColor = person.EyeColor,
                CreatedAt = person.CreatedAt,
                Starships = person.Starships.Select(s => new StarshipDto
                {
                    Id = s.Id,
                    Length = s.Length,
                    Model = s.Model,
                    Name = s.Name
                }).ToList()
            };
        }
    }
}