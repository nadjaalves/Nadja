using Model.Tables;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DAL.Tables
{
    public class CategoryDAL
    {

        private EFContext context = new EFContext();

        public IQueryable<Category>GetOrderedByName()
        {
            return context.Categories.OrderBy(n => n.Name);
        }

        public Category ById(long id)
        {
            return context.Categories.Where(p => p.CategoryId == id).First();
        }
        public void Save(Category category)
        {
            if (category.CategoryId == null)
            {
                context.Categories.Add(category);
            }
            else
            {
                context.Entry(category).State =
                EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Category Delete(long id)
        {
            Category category = ById(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return category;
        }

        public IQueryable<Category> Get()
        {
            return  context.Categories;
        }
    }
}