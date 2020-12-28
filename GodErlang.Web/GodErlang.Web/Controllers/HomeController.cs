using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GodErlang.Web.Models;
using GodErlang.Service;
using GodErlang.Entity.Models;
using GodErlang.Entity;

namespace GodErlang.Web.Controllers
{
    public class HomeController : BaseController
    {
        ProductService productService;

        public HomeController(GodErlangEntities db)
        {
            productService = new ProductService(db);
        }

        public IActionResult Index(ProductExecState? st = null)
        {
            if (st == null)
            {
                ViewBag.State = -1;
            }
            else
            {
                ViewBag.State = (int)st.Value;
            }
            ViewBag.ProductStatus = productService.GetLastMonthStatus(1, st);
            return View();
        }
    }
}
