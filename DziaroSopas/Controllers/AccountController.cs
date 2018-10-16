using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DziaroSopas.Models;
using MongoDB.Driver;
using ServiceLayer;
using Roles = Helpers.Roles;
using System.Net;

namespace DziaroSopas.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService iAccountService)
        {
            _accountService = iAccountService;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(DataContract.GenericDomains.User user)
        {
            if(ModelState.IsValid)
            {
                var newUSer = new DataContract.GenericDomains.User
                {
                    Name = user.Name,
                    Password = user.Password,
                    Email = user.Email,
                    ConfirmPassword = user.ConfirmPassword
                };
                var result = _accountService.Register(newUSer);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AdminModel
                {
                    UserName = model.UserName,
                    Password = model.Password
                };
                var result = _accountService.LoginUser(model.UserName, model.Password);
                if (result != null)
                {
                    var authTicket = new FormsAuthenticationTicket
                    (
                        1,
                        user.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(20),
                        false,
                        result.Role == Roles.Admin ? "Admin" : "NotAdmin"
                    );
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    string currentUser = User.Identity.Name;

                    Response.Cookies.Add(authCookie);
                    if (result.Role == Roles.Admin)
                    {
                        return RedirectToAction("Admin", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}