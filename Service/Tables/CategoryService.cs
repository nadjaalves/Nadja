using Model.Tables;
using Persistence.DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tables
{
    public class CategoryService
    {
        private CategoryDAL categoryDAL = new CategoryDAL();

        public IQueryable<Category>GetOrderedByName()
        {
            return categoryDAL.GetOrderedByName();
        }

        public Category ById(long id)
        {
            return categoryDAL.ById(id);
        }

        public void SaveCategory(Category category)
        {
            categoryDAL.Save(category);
        }

        public Category Delete(long id)
        {
            return categoryDAL.Delete(id);
        }

        public IQueryable<Category> Get()
        {
            return categoryDAL.Get();
        }
    }
}
