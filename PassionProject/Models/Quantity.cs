using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class OrderBurgers
    {
        [Key]
        public int Order_id { get; set; }

        public int Burger_id { get; set; }


    
        public int Quantity { get; set; }
    }


}