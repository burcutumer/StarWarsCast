using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Entities
{
    [Table("People")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Mass { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}