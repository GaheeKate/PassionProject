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

            int maxListCount = 3;
            int pageNum = 1;
            int totalCount = 1;

            if (Request.QueryString["page"] != null)
                pageNum = Convert.ToInt32(Request.QueryString["page"]);

            IEnumerable<OrderDto> orders = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;

            totalCount = orders.Count();

            orders = orders.OrderBy(x => x.Id)
                   .Skip((pageNum - 1) * maxListCount)
                   .Take(maxListCount).ToList();



            ViewBag.Page = pageNum;
            ViewBag.TotalCount = totalCount;
            ViewBag.MaxListCount = maxListCount;




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



            //show associated burgers with this order
            url = "BurgerData/ListBurgersForOrder/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<BurgerxOrderDto> Burgers = response.Content.ReadAsAsync<IEnumerable<BurgerxOrderDto>>().Result;

            ViewModel.OrderedBurgers = Burgers;




            return View(ViewModel);    



    }

        public ActionResult Error() {
            return View();

        }






        //POST: Booking/Associate
        [HttpPost]
        public ActionResult Associate(int BurgerID, int OrderID, int Qty)
        {
   
            //call our api to associate Order with burger
            string url = "BurgerData/AssociateBurgerWithOrder/" + BurgerID + "/" + OrderID + "/" + Qty;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details/" + OrderID);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        //Get: Booking/UnAssociate/{BxTID}?BookingID={BookingID}
        //Deprecated. Use Associate instead to change quantity
        [HttpGet]

        public ActionResult UnAssociate(int id, int OrderId)
        {
 


            //call our api to remove a booking x ticket
            string url = "BurgerData/AssociateBurgerWithOrder/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + OrderId);
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
