using System;
using System.Collections.Generic;
using System.Linq;
using GodErlang.Entity.Models;
using GodErlang.Service;
using Microsoft.AspNetCore.Mvc;

namespace GodErlang.Web.Controllers
{
    public class AccountController : Controller
    {
        UserService userService;
        public AccountController(GodErlangEntities db)
        {
            userService = new UserService(db);
        }

        public IActionResult Signin(string account = null, string pwd = null)
        {
            if (account == null && pwd == null)
                return View();

            try
            {
                System.Threading.Thread.Sleep(3000);
                var user = userService.Login(account, pwd);
                Models.UserSessionManager.SetUser(user);
                return Redirect("/home/index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }
    }
}