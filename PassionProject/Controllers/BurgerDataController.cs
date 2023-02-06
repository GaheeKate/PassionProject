using System;
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

namespace PassionProject.Controllers
{
    public class BurgerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BurgerData/ListBurgers
        [HttpGet]
        public IQueryable<Burger> ListBurgers()
        {
            return db.Burgers;
        }

        // GET: api/BurgerData/FindBurger/5
        [ResponseType(typeof(Burger))]
        [HttpGet]
        public IHttpActionResult FindBurger(int id)
        {
            Burger burger = db.Burgers.Find(id);
            if (burger == null)
            {
                return NotFound();
            }

            return Ok(burger);
        }

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

        // POST: api/BurgerData/DeleteBurger/5
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