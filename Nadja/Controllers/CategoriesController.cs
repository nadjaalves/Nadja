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

namespace Nadja.Controllers
{
    public class CategoriesController : Controller
    {
      
        private CategoryService categoryService = new CategoryService();
      
        // GET: Category
        public ActionResult Index()
        {
           
            return View(categoryService.GetOrderedByName());
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

        private void PopularViewBag(Category category  = null)
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
        }

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
            PopularViewBag();
            return View();       
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            
            return SaveCategory(category);
        }
    
        // GET: Categories/Edit/5
        public ActionResult Edit(long? id)
        {
           
            PopularViewBag(categoryService.ById((long)id));
            return GetViewCategoryById(id);

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
          
            return GetViewCategoryById(id);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(long? id)
        {
            return GetViewCategoryById(id);
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