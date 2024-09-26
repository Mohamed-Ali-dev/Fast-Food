using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Models.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [MaxLength(30)]

        public string Title { get; set; }
    }
}
