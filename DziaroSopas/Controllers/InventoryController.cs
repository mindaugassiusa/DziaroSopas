using DataContract;
using ServiceLayer.Inventory;
using System.Web.Mvc;
using GridMvc;

namespace DziaroSopas.Controllers
{
    public class InventoryController : Controller
    {
        private IInventorySerivce _inventoryService;

        public InventoryController(IInventorySerivce iInventoryService)
        {
            _inventoryService = iInventoryService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetActiveOrders()
        {
            var response = _inventoryService.GetActiveOrders();
            return View(response);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult GetInventory()
        {
            var respones = _inventoryService.GetInventory();
            return View(respones);
        }

        public ActionResult ShowInventoryToCustomer()
        {
            var response = _inventoryService.ShowInventoryToCustomer();
            return View("ShowInventoryToCustomer", response);
        }

        [HttpGet]
        [Authorize (Roles = "Admin")]
        public ActionResult EditActiveOrders(string Id)
        {
            var model = new CustomerModel();

            var order = _inventoryService.GetOrderById(Id);

            return View(order);
        }
        //[HttpGet]
        //public ActionResult 

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult SaveEditedOrder (CustomerModel model)
        {
            var getId = _inventoryService.SaveEditedOrder(model.Id, model);
            return RedirectToAction("GetActiveOrders");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddNewProduct()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddNewProduct (CustomerModel model)
        {
            var newProduct = _inventoryService.AddNewProduct(model);

            return View("Inventory");
        }

        [HttpPost]
        public ActionResult CustomerMadeOrder (CustomerModel model)
        {
            var addOrder = _inventoryService.CustomerMadeOrder(model);

            return RedirectToAction("Index", "Home/Index");
        }

        [HttpGet]
        public ActionResult Order(string Id)
        {
            var model = new CustomerModel();

            var order = _inventoryService.GetOrderById(Id);

            return View(order);
        }
    }
}