using Model.Registers;
using Model.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Service.Registers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Nadja.Controllers
{
    public class SuppliersController : Controller
    {
        private SupplierService supplierService = new SupplierService();

        private async Task<HttpResponseMessage> GetFromAPI(long? id, Action<HttpResponseMessage> action)
        {
            using (var client = new HttpClient())
            {
                var baseUrl = string.Format("{0}://{1}",
                    HttpContext.Request.Url.Scheme,
                     HttpContext.Request.Url.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                var url = "Api/Suppliers";
                if (id != null)
                    url = "Api/Suppliers/" + id;

                var resquest = await client.GetAsync(url);

                if (action != null)
                    action.Invoke(resquest);

                return resquest;

            }

        }

        // GET: Supplier
        public async Task<ActionResult> Index()
        {
            var list = new List<Supplier>();

            var resp = await GetFromAPI(null, response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Supplier>>(result);
                }
            });
            return View(list);
        }



         private ActionResult GetViewSupplierById(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
                }
                Supplier supplier = supplierService.ById((long)id);
                if (supplier == null)
                {
                    return HttpNotFound();
                }
                return View(supplier);
            }

            private void PopularViewBag(Supplier supplier = null)
            {
                if (supplier == null)
                {

                    ViewBag.SupplierId = new SelectList(supplierService.
                    GetOrderedByName(),
                    "SupplierId", "Name");
                }
                else
                {

                    ViewBag.SupplierId = new SelectList(supplierService.
                    GetOrderedByName(),
                    "SupplierId", "Name", supplier.SupplierId);
                }
            }

           private ActionResult Save(Supplier supplier)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        supplierService.Save(supplier);
                        return RedirectToAction("Index");
                    }
                    return View(supplier);
                }
                catch
                {
                    return View(supplier);
                }
            }

        // GET: Create
        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
        
            return Save(supplier);
        }


        // GET: Suppliers/Edit/5
        public ActionResult Edit(long? id)
             {

                PopularViewBag(supplierService.ById((long)id));
                 return GetViewSupplierById(id);
             }


        // POST: Suppliers/Edit/5
          [HttpPost]
           [ValidateAntiForgeryToken]
           public ActionResult Edit(Supplier supplier)
           {

               return Save(supplier);
           }

           // GET: Testes/Details/5
           public async Task<ActionResult> Details(long? id)
           {

               return  GetViewSupplierById(id);
           }

           // GET: Suppliers/Delete/5
           public ActionResult Delete(long? id)
           {

               return GetViewSupplierById(id);
           }

           // POST: Suppliers/Delete/5
           [HttpPost]
           [ValidateAntiForgeryToken]
           public ActionResult Delete(long id)
           {

               try
               {
                   Supplier supplier = supplierService.Delete(id);
                   TempData["Message"] = "Product " + supplier.Name.ToUpper()
                   + " foi removido";
                   return RedirectToAction("Index");
               }
               catch
               {
                   return View();
               }
           }
    }
}
