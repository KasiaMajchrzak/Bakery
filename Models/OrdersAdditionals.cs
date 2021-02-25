using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public class OrdersAdditionals
    {
        [Key]
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int Additional_Id { get; set; }
        public int Quantity { get; set; }
        public string Details { get; set; }
        [ForeignKey("Order_Id")]
        public virtual Order Order { get; set; }
        [ForeignKey("Additional_Id")]
        public virtual Additional Additional { get; set; }
    }
}
