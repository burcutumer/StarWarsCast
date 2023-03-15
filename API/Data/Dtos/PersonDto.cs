using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Mass { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<StarshipDto> Starships { get; set; } = new();
    }
}