using Model.Registers;
using Service.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Nadja.Controllers.API
{
    public class SuppliersController : ApiController
    {
        private SupplierService service = new SupplierService();


        // GET: api/Suppliers
        public IEnumerable<Supplier> Get()
        {
            return service.GetOrderedByName();

        }

         // GET: api/Suppliers/5
        public Supplier Get(long id)
          {
                Supplier supplier = service.ById(id);
                if (supplier == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                return supplier;
            }
     

        // POST: api/Suppliers
        public HttpResponseMessage Post(Supplier supplier)
        {
           
            service.Save(supplier);
            var response = Request.CreateResponse<Supplier>(HttpStatusCode.Created, supplier);
            string uri = Url.Link("DefaultApi", new { id = supplier.SupplierId });
            response.Headers.Location = new Uri(uri);
            return response;
        }
         

    // PUT: api/Suppliers/5
    public void Put(int id, Supplier supplier)
    {
            supplier.SupplierId = id;
            if (supplier != null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            service.Save(supplier);
        }

    // DELETE: api/Suppliers/5
    public void Delete(int id)
          {
            Supplier supplier = service.ById(id);
            if (supplier == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            service.Delete(id);
        }
    }

    }
