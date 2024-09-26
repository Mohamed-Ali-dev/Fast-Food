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
    public class SubCategory
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Sub Category")]

        public string Title { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
