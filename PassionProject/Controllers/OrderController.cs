using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PassionProject.Models;
using PassionProject.Models.ViewModels;





namespace PassionProject.Controllers
{

    public class OrderController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static OrderController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44320/api/");
        }

        // GET: Order/List
        public ActionResult List()
        {   //objective: communicate with our Order data api to retrieve a list of Orders
            //curl https://localhost:44320/api/OrderData/ListOrders

       
            string url = "OrderData/ListOrders";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<OrderDto> orders = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;
            Debug.WriteLine("The Number of orders are ");
            Debug.WriteLine(orders.Count());


            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            DetailsOrder ViewModel = new DetailsOrder();

            //objective: communicate with our Order data api to retrieve one order
            //curl https://localhost:44320/api/OrderData/FindOrder/5


            string url = "OrderData/FindOrder/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            OrderDto SelectedOrder = response.Content.ReadAsAsync<OrderDto>().Result;

            ViewModel.SelectedOrder = SelectedOrder;


       


            return View(ViewModel);    



    }

        public ActionResult Error() {
            return View();

        }

        // GET: Order/New
        public ActionResult New()

            //information about all orders in the system.
            //GET api/orderdata/listorders



        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @order.json https://localhost:44320/api/OrderData/AddOrder

            string url = "OrderData/AddOrder";

            string jsonpayload = jss.Serialize(order);

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

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
         
            string url = "OrderData/FindOrder/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            OrderDto seletedorder = response.Content.ReadAsAsync<OrderDto>().Result;

            return View(seletedorder);

        }


        // POST: Order/Update/5
        [HttpPost]
        public ActionResult Update(int id, Order order)
        {

            string url = "OrderData/UpdateOrder/" + id;

            string jsonpayload = jss.Serialize(order);

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

        // GET: Order/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "OrderData/FindOrder/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            OrderDto seletedorder = response.Content.ReadAsAsync<OrderDto>().Result;

            return View(seletedorder);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "OrderData/DeleteOrder/" + id;

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
