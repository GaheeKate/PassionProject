using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class DetailsBurger
    {

        public BurgerDto SelectedBurger { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }

   


    }
}