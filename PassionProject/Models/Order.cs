using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        //An order belongs to one store
        //A store can have many orders
        [ForeignKey("Location")]
        public int StoreId { get; set; }
        public virtual Location Location { get; set; }

    }

    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int StoreId { get; set; }
 


    }
}