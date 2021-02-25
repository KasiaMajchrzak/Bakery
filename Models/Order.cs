using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public int BaseProduct_Id { get; set; }
        public int Cream_Id { get; set; }
        public int Cake_Id { get; set; }
        public int Servings { get; set; }
        public DateTime OrderedOn { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool IsTemplate { get; set; }
        public string TemplateName { get; set; }
        [ForeignKey("BaseProduct_Id")]
        public virtual BaseProduct BaseProduct { get; set; }
        [ForeignKey("Cream_Id")]
        public virtual Cream Cream { get; set; }
        [ForeignKey("Cake_Id")]
        public virtual Cake Cake { get; set; }
        [NotMapped]
        public List<OrdersAdditionals> OrdersAdditionals { get; set; }
        [NotMapped]
        public List<OrdersDecorations> OrdersDecorations { get; set; }
        [NotMapped]
        public string OrderedOnString { get; set; }
    }
}
