using System;
using System.Collections.Generic;
using GodErlang.Entity.Models;
using GodErlang.Service;
using GodErlang.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GodErlang.Web.Controllers
{
    public class ProductController : BaseController
    {
        ProductService productService;

        public ProductController(GodErlangEntities db)
        {
            productService = new ProductService(db);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(string productUrl)
        {
            try
            {
                productService.AddStatus(1, productUrl);
                return Json(new { state = true, productUrl });
            }
            catch (Exception ex)
            {
                return Json(new { state = false, error = ex.Message });
            }
        }

        public IActionResult List()
        {
            List<ProductDetails> products = productService.GetAll();
            ViewBag.Products = products;
            return View();
        }

        [HttpPost]
        public IActionResult RemoveStatus(int statusId)
        {
            try
            {
                productService.DeleteStatus(1, statusId);
                return Json(new { state = true, statusId });
            }
            catch (Exception ex)
            {
                return Json(new { state = false, error = ex.Message });
            }
        }

        public IActionResult Progress()
        {
            return View();
        }
    }
}