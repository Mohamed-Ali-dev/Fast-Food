using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]

        public OrderHeader orderHeader { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        [ValidateNever]

        public Item Item { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
