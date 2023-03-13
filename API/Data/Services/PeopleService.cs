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

        public Task<Response<PersonDto>> CreatePerson(CreatePersonDto person)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletePerson(int id)
        {
            // Find(id)   kontrol   remove(person)
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
            // Select()   gerek yoksa direk ToList()   kontrol   return new RESPONSE<LIST<DTO>>
            var people = await _context.People
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
                Error = "StarWars Cast could not found"
            };
        }

        public Task<Response<PersonDto>> GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        private static PersonDto MapToPersonDto(Person person)
        {
            return new PersonDto
            {
                Name= person.Name,
                Height = person.Height,
                Mass = person.Mass,
                SkinColor = person.SkinColor,
                EyeColor = person.EyeColor,
            };
        }
    }
}