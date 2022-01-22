using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeShopMVC.Dal
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; }
        [Display(Name = "Product name")]
        public string productId { get; set; }
        public int? price { get; set; }
        [Range(1,20)]
        public int? seat { get; set; }

    }
}