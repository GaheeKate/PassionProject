using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PassionProject.Models
{
    public class BurgerxOrder
    {   //objective:many-many relationship
        //between burger and order
        //a burger belongs to many order
        
        [Key]
        public int BurgerxOrderId { get; set; }


        [ForeignKey("Burger")]
        public int BurgerId { get; set; }
        public decimal BurgerPrice { get; set; }
        public virtual Burger Burger { get; set; }
     


        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }


        //value in numbers associating a quantity to a pair of burger and order
        public int Quantity { get; set; }
    }

    public class BurgerxOrderDto
    {
        public int BurgerxOrderId { get; set; }

        public int BurgerId { get; set; }
        public int OrderId { get; set; }

        public int Quantity { get; set; }
        public decimal BurgerPrice { get; set; }


    }






}