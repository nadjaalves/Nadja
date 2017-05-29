using Model.Registers;
using Persistence.DAL.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Registers
{
    public class ProductService
    {
        private ProductDAL productDAL = new ProductDAL();

        public IQueryable GetOrderedByName()
        {
            return productDAL.GetOrderedByName();
        }
        public Product ById(long id)
        {
            return productDAL.ById(id);
        }
        public void Save(Product product)
        {
            productDAL.Save(product);
        }
        public Product Delete(long id)
        {
            return productDAL.Delete(id);
        }
        public IQueryable<Product> GetProductByCategory(long id)
        {
            return productDAL.ByCategory(id);
        }
    }
}
