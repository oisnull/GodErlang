using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GodErlang.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Signin()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }
    }
}