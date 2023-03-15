using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Dtos
{
    public class CreateStarshipDto
    {
         public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Length { get; set; }
        public int PilotId { get; set; }
    }
}