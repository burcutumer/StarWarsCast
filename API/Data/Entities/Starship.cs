using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Entities
{
    [Table("Starships")]
    public class Starship
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Length { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}