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
    public class Coupon
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public CouponType Type { get; set; }
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        public double Discount { get; set; }
        public double MinimumAmount { get; set; }
        [ValidateNever]
        public string? CouponImage { get; set; }
        public bool IsActive { get; set; }

    }
    public enum CouponType
    {
        percent=0,
        currency = 1 

    }
}
