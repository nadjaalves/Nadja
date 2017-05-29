using Model.Registers;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DAL.Registers
{
    public class SupplierDAL
    {

        private EFContext context = new EFContext();

        public IQueryable<Supplier> GetOrderedByName()
        {
            return context.Suppliers.OrderBy(n => n.Name);
        }

        public Supplier ById(long id)
        {
            return context.Suppliers.Where(p => p.SupplierId == id).First();
        }
        public void Save(Supplier supplier)
        {
            if (supplier.SupplierId == null)
            {
                context.Suppliers.Add(supplier);
            }
            else
            {
                context.Entry(supplier).State =
                EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Supplier Delete(long id)
        {
            Supplier supplier = ById(id);
            context.Suppliers.Remove(supplier);
            context.SaveChanges();
            return supplier;
        }
    }
}