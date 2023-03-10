using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Burger
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //current price of this burger
        public decimal BurgerPrice { get; set; }

    }

    public class BurgerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //current price of this burger
        public decimal BurgerPrice { get; set; }
    }




}