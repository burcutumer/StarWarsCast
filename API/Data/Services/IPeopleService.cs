using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Dtos;

namespace API.Data.Services
{
    public interface IPeopleService
    {
        Task<Response<List<PersonDto>>> GetPeople();
        Task<Response<PersonDto>> GetPerson(int id);
        Task<Response<PersonDto>> CreatePerson(CreatePersonDto person);
        Task<bool> DeletePerson(int id);
        Task<Response<PersonDto>> UpdatePerson(UpdatePersonDto person, int id);
    }
}