using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject.Models;
using PassionProject.Models.ViewModels;
using System.Web.Script.Serialization;





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

            int maxListCount = 3;
            int pageNum = 1;
            int totalCount = 1;

            if (Request.QueryString["page"] != null)
                pageNum = Convert.ToInt32(Request.QueryString["page"]);


            string url = "BurgerData/ListBurgers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<BurgerDto> burgers = response.Content.ReadAsAsync<IEnumerable<BurgerDto>>().Result;


            totalCount = burgers.Count();

            burgers = burgers.OrderBy(x => x.Id)
                   .Skip((pageNum - 1) * maxListCount)
                   .Take(maxListCount).ToList();

        

            ViewBag.Page = pageNum;
            ViewBag.TotalCount = totalCount;
            ViewBag.MaxListCount = maxListCount;


            return View(burgers);
        }






        // GET: Burger/Details/5
        public ActionResult Details(int id)
        {

            DetailsBurger ViewModel = new DetailsBurger();

            //objective: communicate with our Burger data api to retrieve one burger
            //curl https://localhost:44320/api/BurgerData/FindBurger/5

            string url = "BurgerData/FindBurger/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            BurgerDto SelectedBurger = response.Content.ReadAsAsync<BurgerDto>().Result;
            ViewModel.SelectedBurger = SelectedBurger;

            //Get Orders for this burger 
            url = "OrderData/ListOrdersForBurger/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<OrderDto> Orders = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;

            ViewModel.Orders = Orders;


            return View(ViewModel);


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

            BurgerDto seletedburger = response.Content.ReadAsAsync<BurgerDto>().Result;

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

            BurgerDto seletedburger = response.Content.ReadAsAsync<BurgerDto>().Result;

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
