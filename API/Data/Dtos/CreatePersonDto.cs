using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Dtos
{
    public class CreatePersonDto
    {
        public string Name { get; set; } = null!;
        public int Height { get; set; }
        public int Mass { get; set; }
        public string SkinColor { get; set; } = null!;
        public string EyeColor { get; set; } = null!;
        public List<StarshipDto>? Starships { get; set; }
    }
}