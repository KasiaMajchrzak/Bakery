using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public class Cake
    {
        [Key]
        public int Cake_Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Flavour { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
    }
}
