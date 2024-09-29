using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        [DisplayName("Category")]
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [DisplayName("Sub Category")]
        [Required]
        public int SubCategoryId { get; set; }
        [ValidateNever]
        public SubCategory SubCategory { get; set; }
        [ValidateNever]
        public List<ItemImage> ItemImages { get; set; }
    }
}
