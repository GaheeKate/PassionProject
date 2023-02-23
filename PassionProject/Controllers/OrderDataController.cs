using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.Models;
using System.Diagnostics;

using Microsoft.AspNet.Identity;
namespace PassionProject.Controllers
{
    public class OrderDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all orders in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all orders in the database, including their associated species.
        /// </returns>
        /// <example>
        /// GET: api/OrderData/ListOrders
        /// </example>


        [HttpGet]
        [ResponseType(typeof(OrderDto))]

        public IHttpActionResult ListOrders()
        {


            List<Order> Orders = db.Orders.ToList(); ;
            List<OrderDto> OrderDtos = new List<OrderDto>();

            Orders.ForEach(b => OrderDtos.Add(new OrderDto()
            {
                Id = b.Id,
                Date = b.Date,
                StoreId = b.StoreId

            }));

            return Ok(OrderDtos);
        }







        /// <summary>
        /// Returns all orders in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An order in the system matching up to the order ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the order</param>
        /// <example>
        /// GET: api/OrderData/FindOrder/5
        /// </example>
        [HttpGet]
        [ResponseType(typeof(Order))]

        public IHttpActionResult FindOrder(int id)
        {
         
            Order Order = db.Orders.Find(id);
            OrderDto OrderDto = new OrderDto()
            {
                Id = Order.Id,
                Date = Order.Date,
                StoreId = Order.StoreId,
 
            };
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(OrderDto);
        }






        /// <summary>
        /// Updates a particular order in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Order ID primary key</param>
        /// <param name="order">JSON FORM DATA of an order</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/OrderData/UpdateOrder/5
        /// FORM DATA: Order JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {

                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds an order to the system
        /// </summary>
        /// <param name="order">JSON FORM DATA of an order</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Order ID, Order Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/OrderData/AddOrder
        /// FORM DATA: Order JSON Object
        /// </example>
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult AddOrder(Order Order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            db.Orders.Add(Order);
            db.SaveChanges();


            //add one of each burger at 0 qty
            List<Burger> burgers = db.Burgers.ToList();
            burgers.ForEach(t =>
                db.BurgerxOrders.Add(
                    new BurgerxOrder
                    {
                        BurgerId = t.Id,
                        OrderId = Order.Id,
                        Quantity = 0,
    
                    }
                )
            );


            db.SaveChanges();


            return CreatedAtRoute("DefaultApi", new { id = Order.Id }, Order);
        }

        /// <summary>
        /// Deletes an order from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the order</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/OrderData/DeleteOrder/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }



        /// <summary>
        /// Gathers information about all Orders related to a particular Burger ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: Orders in the database
        /// </returns>
        /// <param name="id">Ticket ID.</param>
        /// <example>
        /// GET: api/OrderData/ListOrdersForBurger/3
        /// </example>
        [HttpGet]
        [ResponseType(typeof(BurgerxOrderDto))]
        public IHttpActionResult ListOrdersForBurger(int id)
        {

            List<BurgerxOrder> BxTs = db.BurgerxOrders.Where(bxt => bxt.BurgerId == id).Include(bxt => bxt.Order).ToList();

            List<OrderDto> OrderDtos = new List<OrderDto>();

            BxTs.ForEach(bxt => OrderDtos.Add(new OrderDto()
            {
                Id = bxt.Order.Id,
                Date = bxt.Order.Date,
                StoreId = bxt.Order.StoreId,
               
            }));

            return Ok(OrderDtos);
        }




    }


}
