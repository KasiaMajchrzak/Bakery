using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public class BaseProduct
    {
        [Key]
        public int BaseProduct_Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public Decimal Price { get; set; }
        public string Unit { get; set; }
    }
}
