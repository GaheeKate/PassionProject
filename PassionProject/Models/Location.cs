using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Location
    {
        [Key]
        public int Store_Id { get; set; }

        public string StoreName { get; set; }

        public string StoreLocation { get; set; }
    }
}