using Nadja.Models;
using Service.Registers;
using Service.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nadja.Controllers.API
{
    public class CategoryController : ApiController
    {
        private CategoryService service = new CategoryService();
        private ProductService productservice = new ProductService();

        // GET: api/Categories
        public CategoryListAPIModel Get()
        {
            var apiModel = new CategoryListAPIModel();

            try
            {
                apiModel.Result = service.Get();
            }
            catch (System.Exception)
            {
                apiModel.Message = "OK";

            }
            return apiModel;
        }

    
        // GET: api/Category/5
        public CategoryAPIModel Get(long? id)
        {
            var apiModel = new CategoryAPIModel();

            try
            {
                apiModel.Result = service.ById(id.Value);
                if (apiModel.Result != null)
                    apiModel.Result.Products = productservice.GetProductByCategory(id.Value).ToList();

            }
           catch (Exception)
            {
                apiModel.Message = "!ok";
                
            }
            return apiModel;
        }

        // POST: api/Category
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Category/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
        }
    }
}
