using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeShopMVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public bool? isAdmin { get; set; }
        public bool? isBaresta { get; set; }
       
        
        [Required]
        public string name { get; set; }
       
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        
        [Required]
        public int age { get; set; }
        
        [Required]
        public string username { get; set; }
        
        public bool? vip{ get; set; }
    }
}