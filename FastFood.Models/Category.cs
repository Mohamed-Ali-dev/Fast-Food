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
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Category")]
        public string Title { get; set; }
        [ValidateNever]
        public ICollection<Item> Items { get; set; }
        [ValidateNever]
        public ICollection<SubCategory> SubCategories { get; set; }

    }
}
