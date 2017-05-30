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
using Service.Tables;
using System.Threading.Tasks;
using Nadja.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Nadja.Controllers
{
    public class CategoriesController : Controller
    {
      
        private CategoryService categoryService = new CategoryService();
      
       

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            var apiModel = new CategoryListAPIModel();

            var resp = await GetFromAPI(response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    // { Message: "OK", Result: [{},{}]}
                    string result = response.Content
                    .ReadAsStringAsync().Result;
                    apiModel = JsonConvert
                    .DeserializeObject<CategoryListAPIModel>(result);
                }
            });

            return View(apiModel.Result);
        }



        private ActionResult GetViewCategoryById(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = categoryService.ById((long)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        private async Task<ActionResult> GetViewById(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            CategoryAPIModel item = null;
            var resp = await GetFromAPI(response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content
                    .ReadAsStringAsync().Result;
                    item = JsonConvert
                    .DeserializeObject<CategoryAPIModel>(result);
                }
            }, id.Value);

            if (!resp.IsSuccessStatusCode)
                return new HttpStatusCodeResult(resp.StatusCode);

            if (item.Message == "!OK" ||
                item.Result == null) return HttpNotFound();

            return View(item.Result);
        }

        private async Task<HttpResponseMessage> GetFromAPI(
            Action<HttpResponseMessage> action,
            long? id = null)
        {
            using (var client = new HttpClient())
            {
                var reqUrl = HttpContext.Request.Url;
                var baseUrl = string.Format("{0}://{1}",
                    reqUrl.Scheme, reqUrl.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();

                var url = "Api/Categories";
                if (id != null) url += "/" + id;

                var request = await client.GetAsync(url);
                //HttpContent content = new HttpContent();
                ////content.
                //var r = client.PostAsync(url, content);

                if (action != null)
                    action.Invoke(request);

                return request;
            }
        }

        /*  private void PopularViewBag(Category category  = null)
          {
              if (category == null)
              {
                  ViewBag.CategoryId = new SelectList(categoryService.GetOrderedByName(),
                  "CategoryId", "Name");
              }
              else
              {
                  ViewBag.CategoryId = new SelectList(categoryService.GetOrderedByName(),
                  "CategoryId", "Name", category.CategoryId);
              }
          }*/

        private ActionResult SaveCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryService.SaveCategory(category);
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch
            {
                return View(category);
            }
        }


        // GET: Create
        public ActionResult Create()
    {
            
            return View();       
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            
            return SaveCategory(category);
        }
    
        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {

            return await GetViewById(id);

        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            return SaveCategory(category);
        }

        // GET: Testes/Details/5
        public async Task<ActionResult> Details(long? id)
        {
          
            return await GetViewById(id);
        }


        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            return await GetViewById(id);
        }
        

          // POST: Categories/Delete/5
          [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        { 
          try
            {
                Category category = categoryService.ById(id);
        TempData["Message"] = "Category " + category.Name.ToUpper()
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