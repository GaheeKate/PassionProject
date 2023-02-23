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
    public class BurgerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Burgers in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Burgers in the database
        /// </returns>
        /// <example>
        /// GET: api/BurgerData/ListBurgers
        /// </example>

        [HttpGet]
        [ResponseType(typeof(BurgerDto))]
        public IHttpActionResult ListBurgers()
        {
            List<Burger> Burgers = db.Burgers.ToList();
            List<BurgerDto> BurgerDtos = new List<BurgerDto>();

            Burgers.ForEach(a => BurgerDtos.Add(new BurgerDto()
            {
                Id = a.Id,
                Name = a.Name,
                BurgerPrice = a.BurgerPrice
            }));

            return Ok(BurgerDtos);
        }





        /// <summary>
        /// Gathers information about all Burgers related to a particular Order ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: Burgers in the database
        /// </returns>
        /// <param name="id">Order ID.</param>
        /// <example>
        /// GET: api/BurgerData/ListBurgersForOrder/3
        /// </example>
        [HttpGet]
        [ResponseType(typeof(BurgerxOrderDto))]
        public IHttpActionResult ListBurgersForOrder(int id)
        {

            Order Order = db.Orders.Find(id);

            List<BurgerxOrder> BxTs = db.BurgerxOrders.Where(bxt => bxt.OrderId == id).Include(bxt => bxt.Burger).ToList();

            List<BurgerxOrderDto> BxTDtos = new List<BurgerxOrderDto>();

            BxTs.ForEach(bxt => BxTDtos.Add(new BurgerxOrderDto()
            {
                BurgerxOrderId = bxt.BurgerxOrderId,
                BurgerId = bxt.Burger.Id,
                OrderId = bxt.Order.Id,
                Quantity = bxt.Quantity,
                BurgerPrice = bxt.Burger.BurgerPrice,
            }));

            return Ok(BxTDtos);
        }








        /// <summary>
        /// Associates a particular Order with a particular Burger
        /// </summary>
        /// <param name="BurgerId">The Ticket ID primary key</param>
        /// <param name="OrderId">The Booking ID primary key</param>
        /// <param name="Qty">The number of Tickets</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// or
        /// HEADER: 400 (BAD REQUEST)
        /// </returns>
        /// <example>
        /// POST api/TicketData/AssociateTicketWithBooking/4/3/2
        /// </example>
        [HttpPost]
        [Route("api/BurgerData/AssociateBurgerWithOrder/{BurgerId}/{OrderId}/{Qty}")]

        public IHttpActionResult AssociateTicketWithBooking(int BurgerId, int OrderId, int Qty)
        {
            //no negative quantity
            if (Qty < 0) return BadRequest();



            //Try to Find the ticket
            Burger SelectedBurger = db.Burgers.Find(BurgerId);

            //Try to Find the booking
            Order SelectedOrder = db.Orders.Find(OrderId);

            //if ticket or booking doesn't exist return 404
            if (SelectedBurger == null || SelectedOrder == null)
            {
                return NotFound();
            }

            //try to update an already existing association between the ticket and booking
            BurgerxOrder BurgerxOrder = db.BurgerxOrders.Where(bxt => (bxt.BurgerId == BurgerId && bxt.OrderId == OrderId)).FirstOrDefault();
            if (BurgerxOrder != null)
            {
                BurgerxOrder.Quantity = Qty;
                //assume previous price
            }
            //otherwise add a new association between the ticket and the booking
            else
            {
                //Get the current price of the ticket
                decimal BurgerPrice = SelectedBurger.BurgerPrice;

                //Create a new instance of ticket x booking
                BurgerxOrder NewBxT = new BurgerxOrder()
                {
                    Burger = SelectedBurger,
                    Order = SelectedOrder,
                    Quantity = Qty,
                    BurgerPrice = BurgerPrice
                };
                db.BurgerxOrders.Add(NewBxT);
            }
            db.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Removes an association between a particular Booking and a particular Ticket
        /// function is deprecated (not in use). Just use a different qty with 'AssociateTicketWithBooking'
        /// </summary>
        /// <param name="BxTID">Booking X Ticket Primary key</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/TicketData/AssociateTicketWithBooking/200
        /// </example>
        [HttpPost]
        [Route("api/BurgerData/UnAssociateBurgerWithOrder/{BxTID}")]
        [Authorize]
        public IHttpActionResult UnAssociateBurgerWithOrder(int BxTID)
        {

            //Note: this could also be done with the two FK ticket ID and booking ID
            //find the booking x ticket
            BurgerxOrder SelectedBxT = db.BurgerxOrders.Find(BxTID);
            if (SelectedBxT == null) return NotFound();

            db.BurgerxOrders.Remove(SelectedBxT);
            db.SaveChanges();

            return Ok();
        }






        /// <summary>
        /// Returns all Burgers in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A Burger in the system matching up to the Burger ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Burger</param>
        /// <example>
        /// GET: api/BurgerData/FindBurger/5
        /// </example>
        [ResponseType(typeof(BurgerDto))]
        [HttpGet]
        public IHttpActionResult FindBurger(int id)
        {
            Burger Burger = db.Burgers.Find(id);
            BurgerDto BurgerDto = new BurgerDto()
            {
                Id = Burger.Id,
                Name = Burger.Name,
                BurgerPrice = Burger.BurgerPrice
            };
            if (Burger == null)
            {
                return NotFound();
            }

            return Ok(BurgerDto);
        }








        //// GET: api/BurgerData/FindBurger/5
        //[ResponseType(typeof(Burger))]
        //[HttpGet]
        //public IHttpActionResult FindBurger(int id)
        //{
        //    Burger burger = db.Burgers.Find(id);
        //    if (burger == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(burger);
        //}













        





 



        /// <summary>
        /// Returns all Keepers in the system associated with a particular animal.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Keepers in the database taking care of a particular animal
        /// </returns>
        /// <param name="id">Animal Primary Key</param>
        /// <example>
        /// GET: api/BurgerData/ListKeepersForAnimal/1
        /// </example>
        //[HttpGet]
        //[ResponseType(typeof(BurgerDto))]
        //public IHttpActionResult Listburgersfororder(int id)
        //{

        //    List<BurgerxOrder> BxTs = db.BurgerxOrder.Where(bxt => bxt.TicketID == id).Include(bxt => bxt.Booking).ToList();

        //    List<BurgerDto> BurgerDtos = new List<BurgerDto>();

        //    Burgers.ForEach(k => BurgerDtos.Add(new BurgerDto()
        //    {
        //        Id = k.Id,
        //        Name = k.Name
        //    }));





        //    return Ok(BurgerDtos);
        //}





        // POST: api/BurgerData/UpdateBurger/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBurger(int id, Burger burger)
        {
            Debug.WriteLine("I have reached the update burger method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != burger.Id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter"+id);
                Debug.WriteLine("POST parameter" + burger.Id);
                return BadRequest();
            }

            db.Entry(burger).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BurgerExists(id))
                {
                    Debug.WriteLine("Burger not found");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("Non of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BurgerData/AddBurger
        [ResponseType(typeof(Burger))]
        [HttpPost]
        public IHttpActionResult AddBurger(Burger burger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Burgers.Add(burger);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = burger.Id }, burger);
        }




        /// <summary>
        /// Deletes an Burger from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Burger</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/BurgerData/DeleteBurger/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Burger))]
        [HttpPost]
        public IHttpActionResult DeleteBurger(int id)
        {
            Burger burger = db.Burgers.Find(id);
            if (burger == null)
            {
                return NotFound();
            }

            db.Burgers.Remove(burger);
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

        private bool BurgerExists(int id)
        {
            return db.Burgers.Count(e => e.Id == id) > 0;
        }
    }
}