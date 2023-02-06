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

    public class BurgerController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static BurgerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44320/api/");
        }

        // GET: Burger/List
        public ActionResult List()
        {   //objective: communicate with our Burger data api to retrieve a list of Burgers
            //curl https://localhost:44320/api/BurgerData/ListBurgers

       
            string url = "BurgerData/ListBurgers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Burger> burgers = response.Content.ReadAsAsync<IEnumerable<Burger>>().Result;
            Debug.WriteLine("The Number of burgers are ");
            Debug.WriteLine(burgers.Count());


            return View(burgers);
        }

        // GET: Burger/Details/5
        public ActionResult Details(int id)
        {

            //objective: communicate with our Burger data api to retrieve one burger
            //curl https://localhost:44320/api/BurgerData/FindBurger/5

            
            string url = "BurgerData/FindBurger/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            Burger seletedburger = response.Content.ReadAsAsync<Burger>().Result;

            return View(seletedburger);
        }

        public ActionResult Error() {
            return View();

        }

        // GET: Burger/New
        public ActionResult New()

            //information about all orders in the system.
            //GET api/orderdata/listorders



        {
            return View();
        }

        // POST: Burger/Create
        [HttpPost]
        public ActionResult Create(Burger burger)
        {
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @burger.json https://localhost:44320/api/BurgerData/AddBurger

            string url = "BurgerData/AddBurger";

            string jsonpayload = jss.Serialize(burger);

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

        // GET: Burger/Edit/5
        public ActionResult Edit(int id)
        {
         
            string url = "BurgerData/FindBurger/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Burger seletedburger = response.Content.ReadAsAsync<Burger>().Result;

            return View(seletedburger);

        }


        // POST: Burger/Update/5
        [HttpPost]
        public ActionResult Update(int id, Burger burger)
        {

            string url = "BurgerData/UpdateBurger/" + id;

            string jsonpayload = jss.Serialize(burger);

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

        // GET: Burger/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "BurgerData/FindBurger/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Burger seletedburger = response.Content.ReadAsAsync<Burger>().Result;

            return View(seletedburger);
        }

        // POST: Burger/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "BurgerData/DeleteBurger/" + id;

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
