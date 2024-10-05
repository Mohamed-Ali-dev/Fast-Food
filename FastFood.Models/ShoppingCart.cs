﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        [ValidateNever]
        public Item Item { get; set; }
        public int Count { get; set; }
        public string  ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public double Price { get; set; }
    }
}
