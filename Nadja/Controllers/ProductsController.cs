using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Model.Registers;
using Model.Tables;
using System.Net;
using Service.Registers;
using Service.Tables;

namespace Nadja.Controllers
{
    public class ProductsController : Controller
    {
        private ProductService productService =new ProductService();
        private CategoryService categoryService = new CategoryService();
        private SupplierService supplierService = new SupplierService();


        // GET: Products
        public ActionResult Index()
        {
            
            return View(productService.GetOrderedByName());

        }

        private ActionResult GetViewProductById(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Product product = productService.ById((long)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        private void PopularViewBag(Product product = null)
        {
            if (product == null)
            {
                ViewBag.CategoryId = new SelectList(categoryService.GetOrderedByName(),
                "CategoryId", "Name");
                ViewBag.SupplierId = new SelectList(supplierService.
                GetOrderedByName(),
                "SupplierId", "Name");
            }
            else
            {
                ViewBag.CategoryId = new SelectList(categoryService.GetOrderedByName(),
                "CategoryId", "Name", product.CategoryId);
                ViewBag.SupplierId = new SelectList(supplierService.
                GetOrderedByName(),
                "SupplierId", "Name", product.SupplierId);
            }
        }
        private ActionResult Save(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productService.Save(product);
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
   
            return GetViewProductById(id);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
          
            PopularViewBag();
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            return Save(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
          
            PopularViewBag(productService.ById((long)id));
            return GetViewProductById(id);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
           
            return Save(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
           
            return GetViewProductById(id);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
           
            try
            {
                Product product = productService.Delete(id);
                TempData["Message"] = "Product " + product.Name.ToUpper()
                + " deleted";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
