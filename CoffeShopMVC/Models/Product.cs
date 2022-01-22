using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeShopMVC.Models
{
    public class Product
    {
        [Key]
        public int Pid { get; set; }

        public string Category { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public bool OnSale { get; set; }
        
        public int? NewPrice { get; set; }
        [Required]
        public int Amount { get; set; }

        [Required]
        public string Image { get; set; }
    }
}