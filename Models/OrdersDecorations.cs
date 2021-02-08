using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public class OrdersDecorations
    {
        [Key]
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int Decoration_Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Order_Id")]
        public virtual Order Order { get; set; }
        [ForeignKey("Decoration_Id")]
        public virtual Decoration Decoration { get; set; }
    }
}
