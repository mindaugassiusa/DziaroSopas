using System.Web.Mvc;
using ServiceLayer;
using ServiceLayer.Inventory;
using DataContract;

namespace DziaroSopas.Controllers
{
    public class AdminController : Controller
    {
        private IAccountService _accountService;
        
        public AdminController(IAccountService iAccountService)
        {
            _accountService = iAccountService;
        }
    }
}