using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PassionProject.Models;





namespace PassionProject.Controllers
{

    public class LocationController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static LocationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44320/api/");
        }

        // GET: Location/List
        public ActionResult List()
        {   //objective: communicate with our Location data api to retrieve a list of Locations
            //curl https://localhost:44320/api/LocationData/ListLocations

       
            string url = "LocationData/ListLocations";
            HttpResponseMessage response = client.GetAsync(url).Result;



            IEnumerable<Location> locations = response.Content.ReadAsAsync<IEnumerable<Location>>().Result;



            return View(locations);
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {

            //objective: communicate with our Location data api to retrieve one location
            //curl https://localhost:44320/api/LocationData/FindLocations/5

            
            string url = "LocationData/FindLocations/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            Location seletedlocation = response.Content.ReadAsAsync<Location>().Result;

            return View(seletedlocation);
        }

        public ActionResult Error() {
            return View();

        }

        // GET: Location/New
        public ActionResult New()

            //information about all orders in the system.
            //GET api/orderdata/listorders



        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(Location location)
        {
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @location.json https://localhost:44320/api/LocationData/AddLocation

            string url = "LocationData/AddLocation";

            string jsonpayload = jss.Serialize(location);

            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode) {
                return RedirectToAction("List");
            }
            else 
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
         
            string url = "LocationData/FindLocations/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Location seletedlocation = response.Content.ReadAsAsync<Location>().Result;

            return View(seletedlocation);

        }


        // POST: Location/Update/5
        [HttpPost]
        public ActionResult Update(int id, Location location)
        {

            string url = "LocationData/UpdateLocation/" + id;

            string jsonpayload = jss.Serialize(location);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Location/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "LocationData/FindLocation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Location seletedlocation = response.Content.ReadAsAsync<Location>().Result;

            return View(seletedlocation);
        }

        // POST: Location/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "LocationData/DeleteLocation/" + id;

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
