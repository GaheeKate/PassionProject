using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class DetailsOrder
    {

        public OrderDto SelectedOrder { get; set; }

        public IEnumerable<BurgerxOrderDto> OrderedBurgers { get; set; }


    }
}